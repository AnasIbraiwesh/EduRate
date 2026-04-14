/**
 * EduRate — Universities Listing Page
 */
document.addEventListener('DOMContentLoaded', () => {
  document.getElementById('navbar-placeholder').innerHTML = buildNavbar('universities');
  document.getElementById('footer-placeholder').innerHTML  = buildFooter();

  // Pre-fill search from URL param
  const q = getParam('q');
  if (q) document.getElementById('searchInput').value = q;

  renderGrid();

  document.getElementById('searchInput').addEventListener('input', renderGrid);
  document.getElementById('cityFilter').addEventListener('change', renderGrid);
  document.getElementById('sortSelect').addEventListener('change', renderGrid);
});

function getFilteredSorted() {
  const q    = document.getElementById('searchInput').value.toLowerCase().trim();
  const city = document.getElementById('cityFilter').value;
  const sort = document.getElementById('sortSelect').value;

  let results = UNIVERSITIES.filter(uni => {
    const matchQ    = !q || uni.name.toLowerCase().includes(q) || uni.city.toLowerCase().includes(q);
    const matchCity = !city || uni.city === city;
    return matchQ && matchCity;
  });

  results.sort((a, b) => {
    switch (sort) {
      case 'rating-asc':   return a.overallRating - b.overallRating;
      case 'reviews-desc': return b.reviewCount   - a.reviewCount;
      case 'name-asc':     return a.name.localeCompare(b.name);
      default:             return b.overallRating - a.overallRating;
    }
  });

  return results;
}

function renderGrid() {
  const grid  = document.getElementById('uniGrid');
  const empty = document.getElementById('emptyState');
  const count = document.getElementById('resultsCount');
  const unis  = getFilteredSorted();

  grid.innerHTML = '';

  if (!unis.length) {
    empty.classList.remove('d-none');
    count.textContent = 'No results found';
    return;
  }
  empty.classList.add('d-none');
  count.textContent = `Showing ${unis.length} universit${unis.length === 1 ? 'y' : 'ies'}`;

  unis.forEach(uni => {
    const top3 = Object.entries(uni.ratings)
      .sort(([,a],[,b]) => b - a)
      .slice(0, 3)
      .map(([key, val]) => `
        <div style="display:flex;align-items:center;justify-content:space-between;font-size:12px;margin-bottom:5px">
          <span style="color:var(--gray-400);text-transform:capitalize">${key.replace(/([A-Z])/g,' $1')}</span>
          <div style="display:flex;align-items:center;gap:6px">
            <div style="width:70px;height:4px;background:var(--gray-200);border-radius:2px;overflow:hidden">
              <div style="width:${(val/5)*100}%;height:100%;background:var(--accent-blue);border-radius:2px"></div>
            </div>
            <span style="font-weight:600;color:var(--primary);min-width:24px">${val.toFixed(1)}</span>
          </div>
        </div>`).join('');

    grid.insertAdjacentHTML('beforeend', `
      <div class="col-12 col-md-6 col-lg-4" role="listitem">
        <a href="university-profile.html?id=${uni.id}" class="er-card uni-card h-100" aria-label="${uni.name}">
          <div class="uni-card-header">
            <div class="uni-initial">${uni.shortName.charAt(0)}</div>
            <div class="uni-name">${uni.name}</div>
            <div class="uni-location">
              <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"/><circle cx="12" cy="10" r="3"/></svg>
              ${uni.location}
            </div>
          </div>
          <div class="uni-card-body">
            <div class="uni-card-rating">
              <span class="score">${uni.overallRating.toFixed(1)}</span>
              ${starsHTML(uni.overallRating)}
              <span class="review-count">${uni.reviewCount} reviews</span>
            </div>
            ${top3}
          </div>
          <div class="uni-card-footer">
            <div style="display:flex;align-items:center;gap:8px">
              <span class="sentiment-badge positive" style="font-size:10px">😊 ${uni.sentiment.positive}% Positive</span>
            </div>
            <span class="review-link">View Profile
              <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="5" y1="12" x2="19" y2="12"/><polyline points="12 5 19 12 12 19"/></svg>
            </span>
          </div>
        </a>
      </div>`);
  });
}
