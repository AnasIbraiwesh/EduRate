We need to use Claude as a backend accelerator, not as a random code generator.

Rules:
1. Do not let Claude redesign the project.
2. Do not let Claude replace Identity with custom auth.
3. Do not let Claude rename models/tables unless absolutely necessary.
4. Do not ask Claude to build the whole project at once.
5. Ask Claude for one feature slice at a time.
6. Every output from Claude must match our current project files exactly.
7. If Claude suggests structural changes, stop and show me before applying them.

What you need to give Claude first:
- Program.cs
- ApplicationDbContext.cs
- ApplicationUser.cs
- University.cs
- Professor.cs
- Course.cs
- UniversityReview.cs
- ProfessorReview.cs
- ProofSubmission.cs
- Report.cs
- IdentitySeeder.cs
- appsettings.json with secrets removed
- current migration files if needed

First master prompt:
I am working on an ASP.NET Core backend project with PostgreSQL and Entity Framework Core.

Important constraints:
- Authentication is already integrated using ASP.NET Core Identity.
- The auth user is ApplicationUser.
- Do NOT replace Identity with custom auth.
- Do NOT redesign the schema unless there is a serious issue.
- Do NOT rename existing entities unless necessary.
- Preserve the current architecture.
- We are building API endpoints, not Razor views.
- The backend is for a university/professor review platform with universities, professors, courses, reviews, proof submissions, and reports.

Current source of truth:
[attach/paste current backend files]

Your job:
1. Read the files carefully.
2. Keep the current architecture.
3. Generate only the exact feature I ask for.
4. Return code that fits the current project.
5. Briefly explain what each generated file does.
6. If something conflicts with the current files, point it out instead of silently changing it.

Wait for my next feature request.

After that, use prompts like these one by one:

Prompt 1:
Based on the provided project files, generate University API CRUD using ASP.NET Core controllers and DTOs.
Requirements:
- API only, no Razor views
- Use ApplicationDbContext
- Include create, get all, get by id, update, and soft delete
- Use DTOs for create and update
- Preserve existing model names and relationships
- Return clean JSON responses and proper HTTP status codes
- Do not change auth architecture
- Show all files to create or modify

Prompt 2:
Review the generated University CRUD code against the current project files and tell me if there are any mismatches with Identity, ApplicationDbContext, or model names.

Prompt 3:
Based on the provided project files, generate Professor API CRUD.
Requirements:
- API only
- Professor must reference University correctly
- Include create, get all, get by id, update, and soft delete
- Use DTOs
- Preserve current schema and naming
- Return proper HTTP status codes
- Show all files to create or modify

Prompt 4:
Based on the provided project files, generate Course API CRUD.
Requirements:
- API only
- Course must reference both University and Professor correctly
- Include create, get all, get by id, update, and soft delete
- Use DTOs
- Preserve current schema and naming
- Return proper HTTP status codes
- Show all files to create or modify

Prompt 5:
Based on the provided project files, generate UniversityReview and ProfessorReview submission endpoints.
Requirements:
- API only
- Respect one-review-per-user-per-target rule
- Use Identity ApplicationUser
- Preserve current schema
- Return proper validation errors
- Show all files to create or modify

Prompt 6:
Based on the provided project files, generate ProofSubmission endpoints and admin approval/rejection endpoints.
Requirements:
- API only
- Use current ProofSubmission model
- Admin approval/rejection only
- Preserve Identity setup and current schema
- Show all files to create or modify

Prompt 7:
Based on the provided project files, generate Report submission and admin handling endpoints.
Requirements:
- API only
- Preserve current schema
- Use Identity roles for admin protection
- Show all files to create or modify

Workflow rules:
- One feature at a time
- Never apply Claude output blindly
- After each output, compare it with existing files
- If Claude changes architecture, stop and ask before using itWe need to use Claude as a backend accelerator, not as a random code generator.

Rules:
1. Do not let Claude redesign the project.
2. Do not let Claude replace Identity with custom auth.
3. Do not let Claude rename models/tables unless absolutely necessary.
4. Do not ask Claude to build the whole project at once.
5. Ask Claude for one feature slice at a time.
6. Every output from Claude must match our current project files exactly.
7. If Claude suggests structural changes, stop and show me before applying them.

What you need to give Claude first:
- Program.cs
- ApplicationDbContext.cs
- ApplicationUser.cs
- University.cs
- Professor.cs
- Course.cs
- UniversityReview.cs
- ProfessorReview.cs
- ProofSubmission.cs
- Report.cs
- IdentitySeeder.cs
- appsettings.json with secrets removed
- current migration files if needed

First master prompt:
I am working on an ASP.NET Core backend project with PostgreSQL and Entity Framework Core.

Important constraints:
- Authentication is already integrated using ASP.NET Core Identity.
- The auth user is ApplicationUser.
- Do NOT replace Identity with custom auth.
- Do NOT redesign the schema unless there is a serious issue.
- Do NOT rename existing entities unless necessary.
- Preserve the current architecture.
- We are building API endpoints, not Razor views.
- The backend is for a university/professor review platform with universities, professors, courses, reviews, proof submissions, and reports.

Current source of truth:
[attach/paste current backend files]

Your job:
1. Read the files carefully.
2. Keep the current architecture.
3. Generate only the exact feature I ask for.
4. Return code that fits the current project.
5. Briefly explain what each generated file does.
6. If something conflicts with the current files, point it out instead of silently changing it.

Wait for my next feature request.

After that, use prompts like these one by one:

Prompt 1:
Based on the provided project files, generate University API CRUD using ASP.NET Core controllers and DTOs.
Requirements:
- API only, no Razor views
- Use ApplicationDbContext
- Include create, get all, get by id, update, and soft delete
- Use DTOs for create and update
- Preserve existing model names and relationships
- Return clean JSON responses and proper HTTP status codes
- Do not change auth architecture
- Show all files to create or modify

Prompt 2:
Review the generated University CRUD code against the current project files and tell me if there are any mismatches with Identity, ApplicationDbContext, or model names.

Prompt 3:
Based on the provided project files, generate Professor API CRUD.
Requirements:
- API only
- Professor must reference University correctly
- Include create, get all, get by id, update, and soft delete
- Use DTOs
- Preserve current schema and naming
- Return proper HTTP status codes
- Show all files to create or modify

Prompt 4:
Based on the provided project files, generate Course API CRUD.
Requirements:
- API only
- Course must reference both University and Professor correctly
- Include create, get all, get by id, update, and soft delete
- Use DTOs
- Preserve current schema and naming
- Return proper HTTP status codes
- Show all files to create or modify

Prompt 5:
Based on the provided project files, generate UniversityReview and ProfessorReview submission endpoints.
Requirements:
- API only
- Respect one-review-per-user-per-target rule
- Use Identity ApplicationUser
- Preserve current schema
- Return proper validation errors
- Show all files to create or modify

Prompt 6:
Based on the provided project files, generate ProofSubmission endpoints and admin approval/rejection endpoints.
Requirements:
- API only
- Use current ProofSubmission model
- Admin approval/rejection only
- Preserve Identity setup and current schema
- Show all files to create or modify

Prompt 7:
Based on the provided project files, generate Report submission and admin handling endpoints.
Requirements:
- API only
- Preserve current schema
- Use Identity roles for admin protection
- Show all files to create or modify

Workflow rules:
- One feature at a time
- Never apply Claude output blindly
- After each output, compare it with existing files
- If Claude changes architecture, stop and ask before using it
