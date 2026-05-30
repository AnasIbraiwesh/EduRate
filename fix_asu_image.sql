-- Fix: Add missing ImageUrl for Applied Science Private University
-- Run this on the live PostgreSQL database (Render)

UPDATE "Universities"
SET "ImageUrl" = 'https://www.unirank.org/i/logos-seals/applied-science-private-university-jo-logo-seal.png'
WHERE "Name" = 'Applied Science Private University' AND "IsDeleted" = false;
