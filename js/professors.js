/**
 * EduRate — Professors Listing Page
 */
document.addEventListener('DOMContentLoaded', () => {
  document.getElementById('navbar-placeholder').innerHTML = buildNavbar('professors');
  document.getElementById('footer-placeholder').innerHTML  = buildFooter();

  const q = getParam('q');
  if (q) document.getElementById('searchInput').value = q;

  renderGrid();

  document.getElementById('searchInput').addEventListener('input', renderGrid);
  document.getElementById('deptFilter').addEventListener('change', renderGrid);
  document.getElementById('uniFilter').addEventListener('change', renderGrid);
  document.getElementById('sortSelect').addEventListener('change', renderGrid);
});

function getFilteredSorted() {
  const q    = document.getElementById('searchInput').value.toLowerCase().trim();
  const dept = document.getElementById('deptFilter').value;
  const uni  = document.getElementById('uniFilter').value;
  const sort = document.getElementById('sortSelect').value;

  let results = PROFESSORS.filter(p => {
    const matchQ    = !q || p.name.toLowerCase().includes(q) || p.department.toLowerCase().includes(q);
    const matchDept = !dept || p.department === dept;
    const matchUni  = !uni  || p.universityName === uni;
    return matchQ && matchDept && matchUni;
  });

  results.sort((a, b) => {
    switch (sort) {
      case 'wta-desc':     return b.wouldTakeAgain  - a.wouldTakeAgain;
      case 'reviews-desc': return b.reviewCount     - a.reviewCount;
      case 'name-asc':     return a.name.localeCompare(b.name);
      default:             return b.overallRating   - a.overallRating;
    }
  });

  return results;
}

function renderGrid() {
  const grid  = document.getElementById('profGrid');
  const empty = document.getElementById('emptyState');
  const count = document.getElementById('resultsCount');
  const profs = getFilteredSorted();

  grid.innerHTML = '';

  if (!profs.length) {
    empty.classList.remove('d-none');
    count.textContent = 'No results found';
    return;
  }
  empty.classList.add('d-none');
  count.textContent = `Showing ${profs.length} professor${profs.length === 1 ? '' : 's'}`;

  profs.forEach(prof => {
    const initials   = prof.firstName.charAt(0) + prof.lastName.charAt(0);
    const topCat     = Object.entries(prof.ratings).sort(([,a],[,b]) => b - a)[0];
    const topCatName = { difficulty:'Difficulty', workload:'Workload', communication:'Communication', friendliness:'Friendliness', clarity:'Clarity' }[topCat[0]];

    grid.insertAdjacentHTML('beforeend', `
      <div class="col-12 col-md-6 col-lg-3" role="listitem">
        <a href="professor-profile.html?id=${prof.id}" class="er-card prof-card h-100" aria-label="${prof.name}">
          <div class="prof-card-body">
            <div class="prof-avatar">${initials}</div>
            <div class="prof-name">${prof.name}</div>
            <div class="prof-dept">${prof.department}</div>
            <div style="font-size:11px;color:var(--gray-400);margin-bottom:10px">${prof.universityName}</div>
            <div class="uni-card-rating mb-2">
              <span class="score" style="font-size:20px">${prof.overallRating.toFixed(1)}</span>
              ${starsHTML(prof.overallRating, 13)}
            </div>
            <div class="prof-wta mb-2">
              <svg width="13" height="13" viewBox="0 0 24 24" fill="none" stroke="var(--success)" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14 9V5a3 3 0 0 0-3-3l-4 9v11h11.28a2 2 0 0 0 2-1.7l1.38-9a2 2 0 0 0-2-2.3H14z"/><path d="M7 22H4a2 2 0 0 1-2-2v-7a2 2 0 0 1 2-2h3"/></svg>
              Would Take Again: <span class="wta-pct">${prof.wouldTakeAgain}%</span>
            </div>
            <div style="font-size:11px;color:var(--gray-400)">
              Best: <strong style="color:var(--accent-blue)">${topCatName} (${topCat[1].toFixed(1)})</strong>
            </div>
          </div>
          <div class="uni-card-footer">
            <span style="font-size:11px;color:var(--gray-400)">${prof.reviewCount} reviews</span>
            <span class="review-link" style="font-size:12px">View Profile
              <svg width="11" height="11" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="5" y1="12" x2="19" y2="12"/><polyline points="12 5 19 12 12 19"/></svg>
            </span>
          </div>
        </a>
      </div>`);
  });
}
