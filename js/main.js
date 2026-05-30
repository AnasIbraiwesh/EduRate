/* ============================================================
   EduRate — Shared Utilities & Component Builders
   Called on every page. Requires data.js to be loaded first.
   ============================================================ */

// ── 1. Navbar ─────────────────────────────────────────────────
function buildNavbar(activePage) {
  window._activePage = activePage;
  const user = AUTH.get();
  const tFn = (typeof t === 'function') ? t : (k) => k;
  const pages = [
    { id: 'home',            href: 'index.html',           label: tFn('nav.home') },
    { id: 'universities',    href: 'universities.html',    label: tFn('nav.universities') },
    { id: 'professors',      href: 'professors.html',      label: tFn('nav.professors') },
    { id: 'compare',         href: 'compare.html',         label: tFn('nav.compare') },
    { id: 'recommendations', href: 'recommendations.html', label: tFn('nav.recommendations') },
    { id: 'ranking',         href: 'ranking.html',         label: tFn('nav.ranking') },
    { id: 'faq',             href: 'faq.html',             label: tFn('nav.faq') }
  ];

  const navLinks = pages.map(p =>
    `<li class="nav-item">
       <a class="nav-link${activePage === p.id ? ' active' : ''}" href="${p.href}">${p.label}</a>
     </li>`
  ).join('');

  const authHTML = user
    ? `<div class="dropdown">
         <button class="user-avatar-btn dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false" type="button">
           <div class="avatar-circle">${user.fullName.charAt(0).toUpperCase()}</div>
           <span class="d-none d-sm-inline">${user.fullName.split(' ')[0]}</span>
         </button>
         <ul class="dropdown-menu dropdown-menu-end shadow-sm">
           <li><h6 class="dropdown-header">${user.fullName}</h6></li>
           <li><hr class="dropdown-divider"></li>
           ${user.role?.toLowerCase() === 'admin'
             ? `<li><a class="dropdown-item" href="admin.html"><i class="bi bi-shield-lock me-2"></i>${tFn('nav.admin_dashboard')}</a></li>`
             : user.role?.toLowerCase() === 'professor'
               ? `<li><a class="dropdown-item" href="dashboard.html"><i class="bi bi-speedometer2 me-2"></i>${tFn('nav.dashboard')}</a></li>`
               : `<li><a class="dropdown-item" href="recommendations.html"><i class="bi bi-stars me-2"></i>${tFn('nav.my_recs')}</a></li>
                  <li><a class="dropdown-item" href="settings.html"><i class="bi bi-gear me-2"></i>${tFn('nav.settings')}</a></li>`}
           <li><hr class="dropdown-divider"></li>
           <li><a class="dropdown-item text-danger" href="#" id="logout-btn"><i class="bi bi-box-arrow-right me-2"></i>${tFn('nav.logout')}</a></li>
         </ul>
       </div>`
    : `<div class="d-flex gap-2">
         <a href="login.html" class="btn-nav-outline">${tFn('nav.login')}</a>
         <a href="signup.html" class="btn-nav-solid">${tFn('nav.signup')}</a>
       </div>`;

  const html = `
    <nav class="navbar navbar-expand-lg" aria-label="Main navigation">
      <div class="container">
        <a class="navbar-brand" href="index.html">
          <img src="images/universities/logo/ChatGPT Image May 23, 2026, 12_15_01 AM.png" alt="EduRate logo" style="height:100px;width:auto;margin-right:0px;">
          <span dir="ltr" style="unicode-bidi:isolate">Edu<span style="color:#2E86C1">Rate</span></span>
        </a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navMenu"
          aria-controls="navMenu" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navMenu">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0 ms-3">${navLinks}</ul>
          <div class="navbar-search-wrap mx-3 d-none d-lg-block" role="search">
            <i class="bi bi-search search-icon" aria-hidden="true"></i>
            <input type="search" id="navbar-search" autocomplete="off"
              placeholder="${tFn('nav.search_ph')}"
              aria-label="Search universities or professors">
            <div class="search-dropdown" id="search-dropdown" role="listbox" aria-label="Search results"></div>
          </div>
          <button class="btn btn-sm btn-outline-light me-2 lang-toggle-btn" onclick="I18N.setLanguage(I18N.current === 'en' ? 'ar' : 'en')" aria-label="Switch language" style="white-space:nowrap;min-width:58px">${tFn('lang.switch')}</button>
          <div class="ms-auto">${authHTML}</div>
        </div>
      </div>
    </nav>`;

  const el = document.getElementById('navbar-placeholder');
  if (el) el.innerHTML = html;

  // Wire logout
  document.addEventListener('click', e => {
    if (e.target.closest('#logout-btn')) {
      e.preventDefault();
      API.logout().catch(() => {}).finally(() => {
        AUTH.clear();
        window.location.href = 'index.html';
      });
    }
  });

  initNavSearch();
}

// ── 2. Footer ─────────────────────────────────────────────────
function buildFooter() {
  const tFn = (typeof t === 'function') ? t : (k) => k;
  const html = `
    <footer class="site-footer" role="contentinfo">
      <div class="container">
        <div class="row g-4">
          <div class="col-lg-3 col-md-6">
            <div class="d-flex align-items-center gap-2 mb-12" style="margin-bottom:12px">
              <span style="font-size:22px">🎓</span>
              <span dir="ltr" style="font-family:var(--font-heading);font-size:20px;font-weight:700;color:#fff;unicode-bidi:isolate">Edu<span style="color:#2E86C1">Rate</span></span>
            </div>
            <h5>${tFn('footer.about_title')}</h5>
            <p>${tFn('footer.about_text')}</p>
            <div class="footer-social" aria-label="Social media links">
              <a href="#" aria-label="Twitter"><i class="bi bi-twitter-x"></i></a>
              <a href="#" aria-label="Instagram"><i class="bi bi-instagram"></i></a>
              <a href="#" aria-label="LinkedIn"><i class="bi bi-linkedin"></i></a>
              <a href="#" aria-label="Facebook"><i class="bi bi-facebook"></i></a>
            </div>
          </div>
          <div class="col-lg-2 col-md-6">
            <h5>${tFn('footer.quick_links')}</h5>
            <ul class="footer-links">
              <li><a href="index.html">${tFn('nav.home')}</a></li>
              <li><a href="universities.html">${tFn('nav.universities')}</a></li>
              <li><a href="professors.html">${tFn('nav.professors')}</a></li>
              <li><a href="compare.html">${tFn('nav.compare')}</a></li>
              <li><a href="ranking.html">${tFn('nav.ranking')}</a></li>
              <li><a href="faq.html">${tFn('nav.faq')}</a></li>
            </ul>
          </div>
          <div class="col-lg-4 col-md-6">
            <h5>${tFn('footer.contact')}</h5>
            <ul class="footer-links">
              <li><a href="mailto:support@edurate.jo"><i class="bi bi-envelope me-2"></i>support@edurate.jo</a></li>
              <li><a href="#"><i class="bi bi-geo-alt me-2"></i>${tFn('footer.contact_city')}</a></li>
            </ul>
            <p class="mt-3" style="font-size:13px">${tFn('footer.contact_note')}</p>
          </div>
          <div class="col-lg-3 col-md-6">
            <h5>${tFn('footer.legal')}</h5>
            <ul class="footer-links">
              <li><a href="#">${tFn('footer.privacy')}</a></li>
              <li><a href="#">${tFn('footer.terms')}</a></li>
              <li><a href="#">${tFn('footer.cookies')}</a></li>
              <li><a href="#">${tFn('footer.community')}</a></li>
            </ul>
          </div>
        </div>
        <div class="footer-bottom">
          <p>${tFn('footer.rights')}</p>
        </div>
      </div>
    </footer>`;
  const el = document.getElementById('footer-placeholder');
  if (el) el.innerHTML = html;
}

// ── 3. Star HTML (display mode) ───────────────────────────────
/**
 * @param {number} rating  1–5, allows .5 halves
 * @param {string} size    'sm'|'md'|'lg' — maps to font-size
 */
function starsHTML(rating, size = 'md') {
  const sizeMap = { sm: '14px', md: '16px', lg: '22px' };
  const fontSize = sizeMap[size] || '16px';
  let html = `<span class="stars" style="font-size:${fontSize}" aria-label="${rating} out of 5 stars">`;
  for (let i = 1; i <= 5; i++) {
    if (rating >= i) html += `<span class="star filled" aria-hidden="true">&#9733;</span>`;
    else if (rating >= i - 0.5) html += `<span class="star half" aria-hidden="true">&#9733;</span>`;
    else html += `<span class="star" aria-hidden="true">&#9733;</span>`;
  }
  html += '</span>';
  return html;
}

// ── 4. Rating Color ───────────────────────────────────────────
function ratingColorClass(rating) {
  if (rating >= 4.0) return 'high';
  if (rating >= 3.0) return 'medium';
  return 'low';
}
function ratingBgColor(rating) {
  if (rating >= 4.0) return 'var(--success)';
  if (rating >= 3.0) return 'var(--warning)';
  return 'var(--danger)';
}

// ── 5. Sentiment Badge HTML ───────────────────────────────────
function sentimentBadgeHTML(sentiment) {
  const map = {
    positive: { cls: 'badge-positive', icon: 'bi-hand-thumbs-up-fill', label: () => t('common.sentiment_positive') },
    neutral:  { cls: 'badge-neutral',  icon: 'bi-dash-circle-fill',     label: () => t('common.sentiment_neutral')  },
    negative: { cls: 'badge-negative', icon: 'bi-hand-thumbs-down-fill',label: () => t('common.sentiment_negative') }
  };
  const s = map[sentiment] || map.neutral;
  const lbl = s.label();
  return `<span class="badge-sentiment ${s.cls}" aria-label="Sentiment: ${lbl}">
            <i class="bi ${s.icon}" aria-hidden="true"></i> ${lbl}
          </span>`;
}

// ── 6. Render Review Card (University) ───────────────────────
function renderUniversityReviewCard(review) {
  const avg = +review.ratings.overall.toFixed(1);
  const catRows = UNIVERSITY_CATEGORIES.filter(c => review.ratings[c.key] != null).map(c =>
    `<div class="review-rating-item">
       <span>${c.label}</span>
       <div style="display:flex;align-items:center;gap:4px">
         ${starsHTML(review.ratings[c.key], 'sm')}
         <small style="font-weight:600;color:var(--text-primary)">${review.ratings[c.key]}</small>
       </div>
     </div>`
  ).join('');
  return `
    <article class="review-card" aria-label="Review by ${review.reviewer}">
      <div class="review-header">
        <div class="reviewer-info">
          <div class="reviewer-avatar" aria-hidden="true">${review.reviewer.slice(-2)}</div>
          <div>
            <div class="reviewer-name">${review.reviewer}
              <span class="verified-badge ms-1"><i class="bi bi-patch-check-fill" aria-hidden="true"></i> ${t('common.verified_student')}</span>
            </div>
            <div class="reviewer-meta">${formatDate(review.date)}</div>
          </div>
        </div>
        <div class="d-flex align-items-center gap-2">
          <div class="rating-display">
            ${starsHTML(avg)}
            <span class="rating-value">${avg}</span>
          </div>
          ${sentimentBadgeHTML(review.sentiment)}
        </div>
      </div>
      <div class="review-ratings">${catRows}</div>
      <p class="review-comment">${escapeHTML(review.comment)}</p>
    </article>`;
}

// ── 7. Render Review Card (Professor) ────────────────────────
function renderProfessorReviewCard(review) {
  const r = review.ratings;
  const avg = +(((2 * r.friendliness) + (6 - r.difficulty) + (6 - r.workload)) / 4).toFixed(1);
  const wta = review.wouldTakeAgain == null ? ''
    : review.wouldTakeAgain
      ? `<span class="chip" style="background:#d5f5e3;color:#1e8449"><i class="bi bi-check-circle-fill me-1"></i>${t('common.would_take')}</span>`
      : `<span class="chip" style="background:#fde8e6;color:#c0392b"><i class="bi bi-x-circle-fill me-1"></i>${t('common.would_not_take')}</span>`;

  const replyHTML = review.profReply
    ? `<div class="prof-reply">
         <div class="prof-reply-header">
           <i class="bi bi-patch-check-fill"></i> ${t('common.prof_response')}
         </div>
         <p style="font-size:14px;color:var(--text-secondary);margin:0">${escapeHTML(review.profReply)}</p>
       </div>` : '';

  return `
    <article class="review-card" aria-label="Review by ${review.reviewer}">
      <div class="review-header">
        <div class="reviewer-info">
          <div class="reviewer-avatar" aria-hidden="true">${review.reviewer.slice(-2)}</div>
          <div>
            <div class="reviewer-name">${review.reviewer}
              <span class="verified-badge ms-1"><i class="bi bi-patch-check-fill" aria-hidden="true"></i> ${t('common.verified_student')}</span>
            </div>
            <div class="reviewer-meta">${formatDate(review.date)}${review.course ? ` &middot; <strong>${escapeHTML(review.course)}</strong>` : ''}${review.grade ? ` &middot; Grade: ${review.grade}` : ''}</div>
          </div>
        </div>
        <div class="d-flex align-items-center gap-2 flex-wrap">
          <div class="rating-display">
            ${starsHTML(avg)}
            <span class="rating-value">${avg}</span>
          </div>
          ${sentimentBadgeHTML(review.sentiment)}
        </div>
      </div>
      <div class="review-ratings">
        ${PROFESSOR_CATEGORIES.filter(c => review.ratings[c.key] != null).map(c =>
          `<div class="review-rating-item">
             <span>${c.label}</span>
             <div style="display:flex;align-items:center;gap:4px">
               ${starsHTML(review.ratings[c.key], 'sm')}
               <small style="font-weight:600">${review.ratings[c.key]}</small>
             </div>
           </div>`
        ).join('')}
        <div class="review-rating-item align-self-center">${wta}</div>
      </div>
      <p class="review-comment">${escapeHTML(review.comment)}</p>
      ${replyHTML}
    </article>`;
}

// ── 8. Rating Bar Row HTML ────────────────────────────────────
function ratingBarHTML(label, value, max = 5) {
  const pct = (value / max) * 100;
  const cls = ratingColorClass(value);
  return `
    <div class="rating-bar-row">
      <span class="rating-bar-label">${label}</span>
      <div class="rating-bar-track" role="progressbar" aria-valuenow="${value}" aria-valuemin="0" aria-valuemax="${max}" aria-label="${label}: ${value} out of ${max}">
        <div class="rating-bar-fill ${cls}" style="width:${pct}%"></div>
      </div>
      <span class="rating-bar-value">${value.toFixed(1)}</span>
    </div>`;
}

// ── 9. Toast Notifications ────────────────────────────────────
function showToast(message, type = 'info', duration = 4000) {
  if (type === 'danger' || type === 'warning') type = 'error';
  let container = document.getElementById('toast-container');
  if (!container) {
    container = document.createElement('div');
    container.id = 'toast-container';
    container.className = 'toast-container';
    container.setAttribute('role', 'status');
    container.setAttribute('aria-live', 'polite');
    document.body.appendChild(container);
    container.addEventListener('click', (e) => {
      e.target.closest('.toast-close')?.closest('.toast-item')?.remove();
    });
  }
  const icons = { success: 'bi-check-circle-fill', error: 'bi-exclamation-circle-fill', info: 'bi-info-circle-fill' };
  const id = 'toast-' + Date.now();
  const el = document.createElement('div');
  el.className = `toast-item ${type}`;
  el.id = id;
  el.setAttribute('role', 'alert');
  el.innerHTML = `
    <i class="bi ${icons[type] || icons.info} toast-icon" aria-hidden="true"></i>
    <span class="toast-text">${message}</span>
    <button type="button" class="toast-close" aria-label="Close notification">
      <i class="bi bi-x-lg" aria-hidden="true"></i>
    </button>`;
  container.querySelectorAll('.toast-item').forEach(existing => {
    if (existing.querySelector('.toast-text')?.textContent === message) existing.remove();
  });
  container.appendChild(el);
  if (type !== 'error' && duration > 0) {
    setTimeout(() => el.remove(), duration);
  }
}

// ── 10. Navbar Search Autocomplete ────────────────────────────
function initNavSearch() {
  const input = document.getElementById('navbar-search');
  const dropdown = document.getElementById('search-dropdown');
  if (!input || !dropdown) return;

  let debounceTimer;
  input.addEventListener('input', () => {
    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(() => {
      const q = input.value.trim();
      if (q.length < 2) { dropdown.classList.remove('show'); return; }
      const unis  = searchUniversities(q).slice(0, 4);
      const profs = searchProfessors(q).slice(0, 4);
      if (!unis.length && !profs.length) {
        dropdown.innerHTML = `<div class="p-3 text-center" style="font-size:14px;color:var(--text-secondary)">${t('common.no_results')}</div>`;
      } else {
        let html = '';
        if (unis.length) {
          html += `<div class="search-dropdown-section">
                     <div class="search-dropdown-label">${t('common.search_unis')}</div>
                     ${unis.map(u => `
                       <a class="search-dropdown-item text-decoration-none" href="university-profile.html?id=${u.id}">
                         <div class="item-icon uni" aria-hidden="true">🏛</div>
                         <div><div class="item-text">${u.name}</div><div class="item-sub">${u.city} &middot; ${starsHTML(u.overallRating,'sm')} ${u.overallRating}</div></div>
                       </a>`).join('')}
                   </div>`;
        }
        if (unis.length && profs.length) html += '<div class="search-divider"></div>';
        if (profs.length) {
          html += `<div class="search-dropdown-section">
                     <div class="search-dropdown-label">${t('common.search_profs')}</div>
                     ${profs.map(p => `
                       <a class="search-dropdown-item text-decoration-none" href="professor-profile.html?id=${p.id}">
                         <div class="item-icon prof" aria-hidden="true">👨‍🏫</div>
                         <div><div class="item-text">${p.name}</div><div class="item-sub">${p.department} &middot; ${p.universityName}</div></div>
                       </a>`).join('')}
                   </div>`;
        }
        dropdown.innerHTML = html;
      }
      dropdown.classList.add('show');
    }, 250);
  });

  document.addEventListener('click', e => {
    if (!input.contains(e.target) && !dropdown.contains(e.target)) {
      dropdown.classList.remove('show');
    }
  });

  input.addEventListener('keydown', e => {
    if (e.key === 'Escape') { dropdown.classList.remove('show'); input.blur(); }
  });
}

// ── 11. University Card HTML ──────────────────────────────────
function uniCardHTML(uni) {
  const topCats = UNIVERSITY_CATEGORIES
    .map(c => ({ label: c.label, val: uni.ratings[c.key] }))
    .sort((a, b) => b.val - a.val)
    .slice(0, 3);

  return `
    <div class="col-lg-4 col-md-6">
      <div class="uni-card card-hover h-100">
        <div class="uni-card-header">
          ${uni.image ? `<img src="${uni.image}" style="width:calc(100% + 48px);height:200px;object-fit:cover;display:block;margin-left:-24px;margin-right:-24px;margin-bottom:8px">` : `<div style="font-size:32px;margin-bottom:8px">🏛</div>`}
          <div class="uni-card-name">${uni.name}</div>
          <div class="uni-card-location"><i class="bi bi-geo-alt" aria-hidden="true"></i>${uni.city}, ${uni.country}</div>
        </div>
        <div class="uni-card-body">
          <div class="rating-display mb-2">
            ${starsHTML(uni.overallRating)}
            <span class="rating-value">${uni.overallRating}</span>
            <span class="rating-count">(${uni.totalReviews} reviews)</span>
          </div>
          <div style="margin-top:14px">
            ${topCats.map(c =>
              `<div class="rating-bar-row" style="margin-bottom:8px">
                 <span class="rating-bar-label" style="font-size:12px;min-width:110px">${c.label}</span>
                 <div class="rating-bar-track" style="height:6px">
                   <div class="rating-bar-fill ${ratingColorClass(c.val)}" style="width:${(c.val/5)*100}%"></div>
                 </div>
                 <span class="rating-bar-value" style="font-size:12px">${c.val}</span>
               </div>`
            ).join('')}
          </div>
        </div>
        <div class="uni-card-footer">
          <a href="university-profile.html?id=${uni.id}" class="btn btn-primary btn-sm w-100">View Details</a>
        </div>
      </div>
    </div>`;
}

// ── 12. Professor Card HTML ───────────────────────────────────
function profCardHTML(prof) {
  return `
    <div class="col-lg-4 col-md-6">
      <div class="prof-card card-hover h-100">
        <div class="prof-card-header">
          <div style="font-size:32px;margin-bottom:8px" aria-hidden="true">👨‍🏫</div>
          <div class="prof-card-name">
            ${prof.name}
            ${prof.isVerified ? '<i class="bi bi-patch-check-fill ms-1" style="font-size:16px" title="Verified Professor" aria-label="Verified Professor"></i>' : ''}
          </div>
          <div class="prof-card-dept"><i class="bi bi-mortarboard" aria-hidden="true"></i>${prof.department}</div>
        </div>
        <div class="prof-card-body">
          <div style="font-size:13px;color:var(--text-secondary);margin-bottom:10px">
            <i class="bi bi-building me-1" aria-hidden="true"></i>${prof.universityName}
          </div>
          <div class="rating-display mb-2">
            ${starsHTML(prof.overallRating)}
            <span class="rating-value">${prof.overallRating}</span>
            <span class="rating-count">(${prof.totalReviews} reviews)</span>
          </div>
          <div class="d-flex align-items-center gap-2 mt-3">
            <div class="would-take-again-circle" style="
              width:52px;height:52px;border-radius:50%;flex-shrink:0;
              background:conic-gradient(var(--success) ${prof.wouldTakeAgain * 3.6}deg, var(--star-empty) 0);
              display:flex;align-items:center;justify-content:center;position:relative;">
              <div style="width:38px;height:38px;border-radius:50%;background:var(--surface);position:absolute"></div>
              <span style="position:relative;font-size:11px;font-weight:700">${prof.wouldTakeAgain}%</span>
            </div>
            <span style="font-size:12px;color:var(--text-secondary)">Would Take<br>Again</span>
          </div>
        </div>
        <div class="prof-card-footer">
          <a href="professor-profile.html?id=${prof.id}" class="btn btn-secondary btn-sm w-100">View Profile</a>
        </div>
      </div>
    </div>`;
}

// ── 13. Animate Counter ───────────────────────────────────────
function animateCounter(el, target, duration = 1800) {
  const start = performance.now();
  const update = (now) => {
    const elapsed = now - start;
    const progress = Math.min(elapsed / duration, 1);
    const eased = 1 - Math.pow(1 - progress, 3);
    el.textContent = Math.floor(eased * target).toLocaleString();
    if (progress < 1) requestAnimationFrame(update);
    else el.textContent = target.toLocaleString();
  };
  requestAnimationFrame(update);
}

// ── 14. Offline detection ─────────────────────────────────────
function initOfflineDetector() {
  const banner = document.createElement('div');
  banner.className = 'offline-banner';
  banner.setAttribute('role', 'alert');
  banner.innerHTML = '<i class="bi bi-wifi-off me-2" aria-hidden="true"></i>You appear to be offline. Some features may not be available.';
  document.body.prepend(banner);
  const show = () => banner.classList.add('show');
  const hide = () => banner.classList.remove('show');
  window.addEventListener('offline', show);
  window.addEventListener('online', hide);
  if (!navigator.onLine) show();
}

// ── 15. Interactive Star Rating Input ─────────────────────────
/**
 * Builds an accessible 1–5 star input inside containerId.
 * @param {string} containerId  — element to inject into
 * @param {string} name         — unique input name
 * @param {Function} onChange   — called with selected value
 */
function buildStarInput(containerId, name, onChange) {
  const el = document.getElementById(containerId);
  if (!el) return;
  const labels = ['Terrible', 'Poor', 'Average', 'Very Good', 'Excellent'];
  let html = `<div class="star-input" role="radiogroup" aria-label="Rating from 1 to 5">`;
  for (let i = 5; i >= 1; i--) {
    html += `
      <input type="radio" id="${name}-star${i}" name="${name}" value="${i}" aria-label="${i} star — ${labels[i-1]}">
      <label for="${name}-star${i}" title="${i} — ${labels[i-1]}" aria-hidden="true">&#9733;</label>`;
  }
  html += `</div>`;
  el.innerHTML = html;
  el.querySelectorAll(`input[name="${name}"]`).forEach(input => {
    input.addEventListener('change', () => onChange(parseInt(input.value)));
  });
}

// ── 16. Get URL param ─────────────────────────────────────────
function getParam(key) {
  return new URLSearchParams(window.location.search).get(key);
}

// ── 17. Format date ───────────────────────────────────────────
function formatDate(dateStr) {
  const d = new Date(dateStr);
  return d.toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric' });
}

// ── 18. Escape HTML ───────────────────────────────────────────
function escapeHTML(str) {
  const div = document.createElement('div');
  div.textContent = str || '';
  return div.innerHTML;
}

// ── 19. Sort Reviews ──────────────────────────────────────────
function sortReviews(reviews, method) {
  const copy = [...reviews];
  if (method === 'oldest')  return copy.sort((a, b) => new Date(a.date) - new Date(b.date));
  if (method === 'highest') return copy.sort((a, b) => avgRating(b.ratings) - avgRating(a.ratings));
  if (method === 'lowest')  return copy.sort((a, b) => avgRating(a.ratings) - avgRating(b.ratings));
  return copy.sort((a, b) => new Date(b.date) - new Date(a.date)); // 'recent'
}
function avgRating(ratings) {
  if (ratings.overall != null) return ratings.overall;
  // Professor reviews: same inverting formula used for the displayed overall.
  if (ratings.friendliness != null && ratings.difficulty != null && ratings.workload != null)
    return (2 * ratings.friendliness + (6 - ratings.difficulty) + (6 - ratings.workload)) / 4;
  const vals = Object.values(ratings).filter(v => v != null);
  return vals.length ? vals.reduce((a, b) => a + b, 0) / vals.length : 0;
}

// ── 20. Render Pagination ─────────────────────────────────────
function renderPagination(containerId, total, perPage, current, onPage) {
  const el = document.getElementById(containerId);
  if (!el || total <= perPage) { if (el) el.innerHTML = ''; return; }
  const pages = Math.ceil(total / perPage);
  let html = '<nav aria-label="Reviews pagination"><ul class="pagination justify-content-center flex-wrap">';
  html += `<li class="page-item${current===1?' disabled':''}">
             <button class="page-link" data-page="${current-1}" aria-label="Previous">&#8249;</button></li>`;
  for (let i = 1; i <= pages; i++) {
    html += `<li class="page-item${i===current?' active':''}">
               <button class="page-link" data-page="${i}" aria-label="Page ${i}">${i}</button></li>`;
  }
  html += `<li class="page-item${current===pages?' disabled':''}">
             <button class="page-link" data-page="${current+1}" aria-label="Next">&#8250;</button></li>`;
  html += '</ul></nav>';
  el.innerHTML = html;
  el.querySelectorAll('.page-link:not([disabled])').forEach(btn => {
    btn.addEventListener('click', () => {
      const p = parseInt(btn.dataset.page);
      if (p >= 1 && p <= pages) onPage(p);
    });
  });
}

// ── Init on load ──────────────────────────────────────────────
document.addEventListener('DOMContentLoaded', () => {
  initOfflineDetector();
});
