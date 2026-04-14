/**
 * EduRate — Shared JS Utilities
 * Loaded on every page.
 */

document.addEventListener('DOMContentLoaded', () => {
  initNavActiveState();
  initSearchAutocomplete();
  initToastSystem();
});

// ── Navbar: mark active link ──────────────────────────────────
function initNavActiveState() {
  const path = window.location.pathname.split('/').pop() || 'index.html';
  document.querySelectorAll('.er-navbar .nav-link').forEach(link => {
    const href = link.getAttribute('href');
    if (href && href !== '#' && path.includes(href.replace('.html', ''))) {
      link.classList.add('active');
    }
  });
}

// ── Global search autocomplete (navbar) ──────────────────────
function initSearchAutocomplete() {
  const input    = document.getElementById('navSearchInput');
  const dropdown = document.getElementById('navSearchDropdown');
  if (!input || !dropdown) return;

  let debounceTimer;

  input.addEventListener('input', () => {
    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(() => {
      const q = input.value.trim();
      if (q.length < 2) { dropdown.classList.remove('show'); return; }
      const results = searchAll(q);
      if (!results.length) { dropdown.classList.remove('show'); return; }
      dropdown.innerHTML = results.map(r => `
        <div class="er-autocomplete-item" data-type="${r.type}" data-id="${r.id}">
          <span class="item-type">${r.type === 'university' ? 'Uni' : 'Prof'}</span>
          <div>
            <div style="font-weight:600">${r.name}</div>
            <div style="font-size:11px;color:var(--gray-400)">${r.sub}</div>
          </div>
        </div>
      `).join('');
      dropdown.classList.add('show');

      dropdown.querySelectorAll('.er-autocomplete-item').forEach(item => {
        item.addEventListener('click', () => {
          const type = item.dataset.type;
          const id   = item.dataset.id;
          window.location.href = type === 'university'
            ? `university-profile.html?id=${id}`
            : `professor-profile.html?id=${id}`;
        });
      });
    }, 200);
  });

  input.addEventListener('keydown', e => {
    if (e.key === 'Escape') { dropdown.classList.remove('show'); input.blur(); }
    if (e.key === 'Enter') {
      const q = input.value.trim();
      if (q) window.location.href = `universities.html?q=${encodeURIComponent(q)}`;
    }
  });

  document.addEventListener('click', e => {
    if (!input.contains(e.target) && !dropdown.contains(e.target)) {
      dropdown.classList.remove('show');
    }
  });
}

// ── Toast System ──────────────────────────────────────────────
function initToastSystem() {
  window.showToast = (title, message, type = 'success') => {
    let toast = document.getElementById('erToast');
    if (!toast) {
      toast = document.createElement('div');
      toast.id = 'erToast';
      toast.className = 'er-toast';
      toast.innerHTML = `
        <div class="er-toast-icon" id="erToastIcon"></div>
        <div class="er-toast-body">
          <div class="er-toast-title" id="erToastTitle"></div>
          <div class="er-toast-msg"   id="erToastMsg"></div>
        </div>`;
      document.body.appendChild(toast);
    }

    toast.className = `er-toast ${type === 'error' ? 'error' : ''}`;
    document.getElementById('erToastTitle').textContent = title;
    document.getElementById('erToastMsg').textContent   = message;
    document.getElementById('erToastIcon').innerHTML    =
      type === 'success'
        ? `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="var(--success)" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><polyline points="20 6 9 17 4 12"/></svg>`
        : `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="var(--danger)" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><line x1="12" y1="8" x2="12" y2="12"/><line x1="12" y1="16" x2="12.01" y2="16"/></svg>`;

    requestAnimationFrame(() => toast.classList.add('show'));
    setTimeout(() => { toast.classList.remove('show'); }, 4000);
  };
}

// ── Shared: render rating bar ─────────────────────────────────
function renderRatingBar(label, iconSvg, score, container) {
  const color   = ratingBarColor(score);
  const pct     = (score / 5) * 100;
  const barHTML = `
    <div class="rating-bar-row">
      <div class="rating-bar-label">
        ${iconSvg}
        <span>${label}</span>
      </div>
      <div class="rating-bar-track">
        <div class="rating-bar-fill ${color}" style="width:0" data-width="${pct}"></div>
      </div>
      <div class="rating-bar-score">${score.toFixed(1)}</div>
    </div>`;
  container.insertAdjacentHTML('beforeend', barHTML);
}

// Animate all rating bars in viewport
function animateRatingBars() {
  document.querySelectorAll('.rating-bar-fill[data-width]').forEach(bar => {
    const width = bar.dataset.width;
    setTimeout(() => { bar.style.width = width + '%'; }, 100);
  });
}

// ── Shared: render review card ────────────────────────────────
function renderReviewCard(review, container, showCourse = false) {
  const courseTag = showCourse && review.course
    ? `<span class="course-tag">${review.course}</span>`
    : '';
  const card = `
    <article class="review-card">
      <div class="review-card-header">
        <div class="review-card-meta">
          <div class="review-card-stars">${starsHTML(review.stars)}</div>
          ${sentimentBadgeHTML(review.sentiment)}
          ${courseTag}
        </div>
        <span class="review-card-date">${review.date}</span>
      </div>
      <div class="review-card-body">
        <p class="review-card-text">${escapeHTML(review.comment)}</p>
        <button class="review-read-more" onclick="toggleReadMore(this)">Read more</button>
      </div>
      <div class="review-card-footer">
        ${review.verified
          ? `<span class="verified-badge">
               <svg width="13" height="13" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><polyline points="20 6 9 17 4 12"/></svg>
               Verified Student
             </span>`
          : '<span style="font-size:11px;color:var(--gray-400)">Unverified</span>'
        }
        <button class="helpful-btn" onclick="toggleHelpful(this, ${review.id})">
          <svg width="13" height="13" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14 9V5a3 3 0 0 0-3-3l-4 9v11h11.28a2 2 0 0 0 2-1.7l1.38-9a2 2 0 0 0-2-2.3H14z"/><path d="M7 22H4a2 2 0 0 1-2-2v-7a2 2 0 0 1 2-2h3"/></svg>
          Helpful · <span class="helpful-count">${review.helpful}</span>
        </button>
      </div>
    </article>`;
  container.insertAdjacentHTML('beforeend', card);
}

// ── Toggle review "Read more" ─────────────────────────────────
function toggleReadMore(btn) {
  const text = btn.previousElementSibling;
  const expanded = text.classList.toggle('expanded');
  btn.textContent = expanded ? 'Show less' : 'Read more';
}

// ── Toggle helpful button ─────────────────────────────────────
function toggleHelpful(btn, reviewId) {
  const active = btn.classList.toggle('active');
  const countEl = btn.querySelector('.helpful-count');
  let count = parseInt(countEl.textContent, 10);
  countEl.textContent = active ? count + 1 : count - 1;
}

// ── HTML escape ───────────────────────────────────────────────
function escapeHTML(str) {
  return str
    .replace(/&/g, '&amp;')
    .replace(/</g, '&lt;')
    .replace(/>/g, '&gt;')
    .replace(/"/g, '&quot;');
}

// ── URL query param helper ────────────────────────────────────
function getParam(key) {
  return new URLSearchParams(window.location.search).get(key);
}

// ── Shared navbar HTML ────────────────────────────────────────
// (Injected by each page's own <header> — kept here as reference only)

// ── Icon helpers (inline SVG strings) ────────────────────────
const ICONS = {
  mapPin:      `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"/><circle cx="12" cy="10" r="3"/></svg>`,
  award:       `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="8" r="6"/><path d="M15.477 12.89 17 22l-5-3-5 3 1.523-9.11"/></svg>`,
  shield:      `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/></svg>`,
  coffee:      `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M17 8h1a4 4 0 1 1 0 8h-1"/><path d="M3 8h14v9a4 4 0 0 1-4 4H7a4 4 0 0 1-4-4Z"/><line x1="6" y1="2" x2="6" y2="4"/><line x1="10" y1="2" x2="10" y2="4"/><line x1="14" y1="2" x2="14" y2="4"/></svg>`,
  building:    `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="2" y="7" width="20" height="14" rx="2" ry="2"/><path d="M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16"/></svg>`,
  users:       `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg>`,
  heart:       `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z"/></svg>`,
  smile:       `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><path d="M8 13s1.5 2 4 2 4-2 4-2"/><line x1="9" y1="9" x2="9.01" y2="9"/><line x1="15" y1="9" x2="15.01" y2="9"/></svg>`,
  wifi:        `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M5 12.55a11 11 0 0 1 14.08 0"/><path d="M1.42 9a16 16 0 0 1 21.16 0"/><path d="M8.53 16.11a6 6 0 0 1 6.95 0"/><line x1="12" y1="20" x2="12.01" y2="20"/></svg>`,
  briefcase:   `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="2" y="7" width="20" height="14" rx="2" ry="2"/><path d="M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16"/></svg>`,
  search:      `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="11" cy="11" r="8"/><line x1="21" y1="21" x2="16.65" y2="16.65"/></svg>`,
  arrowRight:  `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="5" y1="12" x2="19" y2="12"/><polyline points="12 5 19 12 12 19"/></svg>`,
  graduationCap:`<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M22 10v6M2 10l10-5 10 5-10 5z"/><path d="M6 12v5c3 3 9 3 12 0v-5"/></svg>`,
  trophy:      `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="8 21 12 17 16 21"/><line x1="12" y1="17" x2="12" y2="11"/><path d="M7 4H4a2 2 0 0 0-2 2v4c0 2.5 2 4.5 4.5 5.5"/><path d="M17 4h3a2 2 0 0 1 2 2v4c0 2.5-2 4.5-4.5 5.5"/><rect x="7" y="2" width="10" height="9" rx="1"/></svg>`,
};

// ── Shared navbar HTML builder ────────────────────────────────
function buildNavbar(activePage) {
  return `
  <nav class="navbar navbar-expand-lg er-navbar fixed-top">
    <div class="container">
      <a class="navbar-brand" href="index.html">Edu<span>Rate</span></a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navMenu" aria-controls="navMenu" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navMenu">
        <ul class="navbar-nav me-auto ms-3 gap-1">
          <li class="nav-item"><a class="nav-link ${activePage==='home'?'active':''}" href="index.html">Home</a></li>
          <li class="nav-item"><a class="nav-link ${activePage==='universities'?'active':''}" href="universities.html">Universities</a></li>
          <li class="nav-item"><a class="nav-link ${activePage==='professors'?'active':''}" href="professors.html">Professors</a></li>
          <li class="nav-item"><a class="nav-link ${activePage==='compare'?'active':''}" href="compare-universities.html">Compare</a></li>
          <li class="nav-item"><a class="nav-link ${activePage==='faq'?'active':''}" href="faq.html">FAQ</a></li>
        </ul>
        <div class="d-flex align-items-center gap-3">
          <div class="er-nav-search d-none d-lg-block">
            <span class="search-icon">${ICONS.search}</span>
            <input type="text" id="navSearchInput" placeholder="Search universities, professors..." autocomplete="off" aria-label="Search">
            <div class="er-autocomplete" id="navSearchDropdown"></div>
          </div>
          <a href="login.html"  class="btn-er-ghost" style="color:rgba(255,255,255,0.8)">Log In</a>
          <a href="signup.html" class="btn-er-primary" style="height:36px;padding:0 16px;font-size:13px">Sign Up</a>
        </div>
      </div>
    </div>
  </nav>`;
}

// ── Shared footer HTML ────────────────────────────────────────
function buildFooter() {
  return `
  <footer class="er-footer">
    <div class="container">
      <div class="row g-4 mb-4">
        <div class="col-lg-4">
          <div class="footer-brand">Edu<span>Rate</span></div>
          <p class="footer-desc">Helping students make informed academic decisions through honest, verified peer reviews of universities and professors across Jordan.</p>
        </div>
        <div class="col-6 col-lg-2">
          <div class="footer-heading">Platform</div>
          <ul class="footer-links">
            <li><a href="universities.html">Universities</a></li>
            <li><a href="professors.html">Professors</a></li>
            <li><a href="compare-universities.html">Compare</a></li>
          </ul>
        </div>
        <div class="col-6 col-lg-2">
          <div class="footer-heading">Support</div>
          <ul class="footer-links">
            <li><a href="faq.html">FAQ</a></li>
            <li><a href="#">Contact Us</a></li>
            <li><a href="#">Report Issue</a></li>
          </ul>
        </div>
        <div class="col-6 col-lg-2">
          <div class="footer-heading">Legal</div>
          <ul class="footer-links">
            <li><a href="#">Privacy Policy</a></li>
            <li><a href="#">Terms of Use</a></li>
            <li><a href="#">Cookie Policy</a></li>
          </ul>
        </div>
        <div class="col-6 col-lg-2">
          <div class="footer-heading">About</div>
          <ul class="footer-links">
            <li><a href="#">Our Team</a></li>
            <li><a href="#">Capstone Project</a></li>
            <li><a href="#">Feedback</a></li>
          </ul>
        </div>
      </div>
      <div class="footer-bottom">
        <span>© 2024 EduRate. Capstone II Project — University of Jordan.</span>
        <span>Built by Osama Jankoot · Anas Ibraiwish · Saif Hamadeh</span>
      </div>
    </div>
  </footer>`;
}

// ── Page body padding-top (for fixed navbar) ──────────────────
document.documentElement.style.setProperty('--navbar-h', '64px');
document.addEventListener('DOMContentLoaded', () => {
  const main = document.querySelector('main');
  if (main) main.style.paddingTop = '64px';
});
