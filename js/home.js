/**
 * EduRate — Home Page JS
 */
document.addEventListener('DOMContentLoaded', () => {
  document.getElementById('navbar-placeholder').innerHTML = buildNavbar('home');
  document.getElementById('footer-placeholder').innerHTML  = buildFooter();

  renderUniversityCards();
  renderProfessorCards();
  initCountUp();
  initHeroSearch();
});

// ── University Cards (horizontal scroll) ─────────────────────
function renderUniversityCards() {
  const container = document.getElementById('uniCards');
  const sorted = [...UNIVERSITIES].sort((a, b) => b.overallRating - a.overallRating);

  sorted.forEach(uni => {
    const top3 = Object.entries(uni.ratings)
      .sort(([,a],[,b]) => b - a)
      .slice(0, 3)
      .map(([key, val]) => `
        <div style="display:flex;align-items:center;justify-content:space-between;font-size:12px;margin-bottom:6px">
          <span style="color:var(--gray-400);text-transform:capitalize">${key.replace(/([A-Z])/g,' $1')}</span>
          <div style="display:flex;align-items:center;gap:8px">
            <div style="width:80px;height:5px;background:var(--gray-200);border-radius:3px;overflow:hidden">
              <div style="width:${(val/5)*100}%;height:100%;background:var(--accent-blue);border-radius:3px"></div>
            </div>
            <span style="font-weight:600;color:var(--primary)">${val.toFixed(1)}</span>
          </div>
        </div>`).join('');

    container.insertAdjacentHTML('beforeend', `
      <a href="university-profile.html?id=${uni.id}" class="er-card uni-card" role="listitem" aria-label="${uni.name}">
        <div class="uni-card-header">
          <div class="uni-initial">${uni.shortName.charAt(0)}</div>
          <div class="uni-name">${uni.name}</div>
          <div class="uni-location">
            <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"/><circle cx="12" cy="10" r="3"/></svg>
            ${uni.city}
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
          <span class="review-link">View Profile
            <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="5" y1="12" x2="19" y2="12"/><polyline points="12 5 19 12 12 19"/></svg>
          </span>
        </div>
      </a>`);
  });
}

// ── Professor Cards (grid) ────────────────────────────────────
function renderProfessorCards() {
  const container = document.getElementById('profCards');
  const sorted = [...PROFESSORS].sort((a, b) => b.overallRating - a.overallRating).slice(0, 4);

  sorted.forEach(prof => {
    const initials = prof.firstName.charAt(0) + prof.lastName.charAt(0);
    container.insertAdjacentHTML('beforeend', `
      <div class="col-6 col-lg-3" role="listitem">
        <a href="professor-profile.html?id=${prof.id}" class="er-card prof-card" aria-label="${prof.name}">
          <div class="prof-card-body">
            <div class="prof-avatar">${initials}</div>
            <div class="prof-name">${prof.name}</div>
            <div class="prof-dept">${prof.department} · ${prof.universityName}</div>
            <div class="uni-card-rating mb-2">
              <span class="score" style="font-size:18px">${prof.overallRating.toFixed(1)}</span>
              ${starsHTML(prof.overallRating, 14)}
            </div>
            <div class="prof-wta">
              <svg width="13" height="13" viewBox="0 0 24 24" fill="none" stroke="var(--success)" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14 9V5a3 3 0 0 0-3-3l-4 9v11h11.28a2 2 0 0 0 2-1.7l1.38-9a2 2 0 0 0-2-2.3H14z"/><path d="M7 22H4a2 2 0 0 1-2-2v-7a2 2 0 0 1 2-2h3"/></svg>
              Would Take Again: <span class="wta-pct">${prof.wouldTakeAgain}%</span>
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

// ── Count-up animation ────────────────────────────────────────
function initCountUp() {
  const els = document.querySelectorAll('.stat-number[data-target]');
  const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
      if (!entry.isIntersecting) return;
      const el     = entry.target;
      const target = parseInt(el.dataset.target, 10);
      const suffix = el.dataset.suffix || '';
      const duration = 1200;
      const step = target / (duration / 16);
      let current = 0;
      const timer = setInterval(() => {
        current = Math.min(current + step, target);
        el.textContent = Math.floor(current).toLocaleString() + suffix;
        if (current >= target) clearInterval(timer);
      }, 16);
      observer.unobserve(el);
    });
  }, { threshold: 0.5 });
  els.forEach(el => observer.observe(el));
}

// ── Hero search ───────────────────────────────────────────────
function initHeroSearch() {
  const input    = document.getElementById('heroSearch');
  const dropdown = document.getElementById('heroAutocomplete');
  if (!input) return;

  let timer;
  input.addEventListener('input', () => {
    clearTimeout(timer);
    timer = setTimeout(() => {
      const q = input.value.trim();
      if (q.length < 2) { dropdown.classList.remove('show'); return; }
      const results = searchAll(q);
      if (!results.length) { dropdown.classList.remove('show'); return; }
      dropdown.innerHTML = results.map(r => `
        <div class="er-autocomplete-item" onclick="window.location.href='${r.type==='university'?'university-profile':'professor-profile'}.html?id=${r.id}'">
          <span class="item-type">${r.type === 'university' ? 'Uni' : 'Prof'}</span>
          <div>
            <div style="font-weight:600">${r.name}</div>
            <div style="font-size:11px;color:var(--gray-400)">${r.sub}</div>
          </div>
        </div>`).join('');
      dropdown.classList.add('show');
    }, 200);
  });

  input.addEventListener('keydown', e => {
    if (e.key === 'Enter') doHeroSearch();
    if (e.key === 'Escape') dropdown.classList.remove('show');
  });

  document.addEventListener('click', e => {
    if (!input.contains(e.target) && !dropdown.contains(e.target)) dropdown.classList.remove('show');
  });
}

function doHeroSearch() {
  const q = document.getElementById('heroSearch').value.trim();
  if (q) window.location.href = `universities.html?q=${encodeURIComponent(q)}`;
}
