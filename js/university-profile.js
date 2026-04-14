/**
 * EduRate — University Profile Page
 */

const UNI_CATEGORY_META = [
  { key: 'location',      label: 'Location',      icon: ICONS.mapPin },
  { key: 'reputation',    label: 'Reputation',    icon: ICONS.award },
  { key: 'safety',        label: 'Safety',        icon: ICONS.shield },
  { key: 'food',          label: 'Food',          icon: ICONS.coffee },
  { key: 'facilities',    label: 'Facilities',    icon: ICONS.building },
  { key: 'clubs',         label: 'Clubs',         icon: ICONS.users },
  { key: 'happiness',     label: 'Happiness',     icon: ICONS.smile },
  { key: 'internet',      label: 'Internet',      icon: ICONS.wifi },
  { key: 'opportunities', label: 'Opportunities', icon: ICONS.briefcase },
  { key: 'socialLife',    label: 'Social Life',   icon: ICONS.heart },
];

let currentUni = null;

document.addEventListener('DOMContentLoaded', () => {
  document.getElementById('navbar-placeholder').innerHTML = buildNavbar('universities');
  document.getElementById('footer-placeholder').innerHTML  = buildFooter();

  const id = getParam('id');
  currentUni = getUniversityById(id);

  if (!currentUni) {
    document.getElementById('uniName').textContent = 'University not found';
    return;
  }

  document.title = `${currentUni.name} — EduRate`;
  renderHeader();
  renderDescription();
  renderCategoryBars();
  renderScoreCard();
  renderSentimentChart();
  renderQuickStats();
  renderReviews();
  setupReviewSort();
  setupCategorySliders();
  setupReviewCharCount();

  document.getElementById('compareBtn').href      = `compare-universities.html?a=${currentUni.id}`;
  document.getElementById('compareCTABtn').href   = `compare-universities.html?a=${currentUni.id}`;
});

function renderHeader() {
  document.getElementById('breadcrumbName').textContent = currentUni.shortName;
  document.getElementById('uniName').textContent = currentUni.name;

  document.getElementById('uniMeta').innerHTML = `
    <span>
      <svg width="13" height="13" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"/><circle cx="12" cy="10" r="3"/></svg>
      ${currentUni.location}
    </span>
    <span>${starsHTML(currentUni.overallRating, 14)}</span>
    <span style="font-weight:700;color:var(--primary)">${currentUni.overallRating.toFixed(1)}</span>
    <span>${currentUni.reviewCount} reviews</span>
    <span>Est. ${currentUni.established}</span>`;
}

function renderDescription() {
  document.getElementById('uniDescription').innerHTML = `
    <h2 class="heading-2 mb-2">About ${currentUni.name}</h2>
    <p style="font-size:14px;color:var(--gray-700);line-height:1.7;margin:0">${currentUni.description}</p>`;
}

function renderCategoryBars() {
  const container = document.getElementById('categoryBars');
  container.innerHTML = '';
  UNI_CATEGORY_META.forEach(({ key, label, icon }) => {
    const score = currentUni.ratings[key];
    const pct   = (score / 5) * 100;
    const color = ratingBarColor(score);
    container.insertAdjacentHTML('beforeend', `
      <div class="rating-bar-row">
        <div class="rating-bar-label">
          ${icon}
          <span>${label}</span>
        </div>
        <div class="rating-bar-track">
          <div class="rating-bar-fill ${color}" data-width="${pct}" style="width:0"></div>
        </div>
        <div class="rating-bar-score">${score.toFixed(1)}</div>
      </div>`);
  });
  setTimeout(() => {
    document.querySelectorAll('.rating-bar-fill[data-width]').forEach(bar => {
      bar.style.width = bar.dataset.width + '%';
    });
  }, 150);
}

function renderScoreCard() {
  document.getElementById('overallScore').innerHTML =
    `${currentUni.overallRating.toFixed(1)}<span class="score-outof">/5</span>`;
  document.getElementById('overallStars').innerHTML  = starsHTML(currentUni.overallRating, 20);
  document.getElementById('reviewCount').textContent = `Based on ${currentUni.reviewCount} reviews`;
}

function renderSentimentChart() {
  const { positive, neutral, negative } = currentUni.sentiment;
  const ctx = document.getElementById('sentimentChart').getContext('2d');

  new Chart(ctx, {
    type: 'doughnut',
    data: {
      labels: ['Positive', 'Neutral', 'Negative'],
      datasets: [{
        data: [positive, neutral, negative],
        backgroundColor: ['#10B981', '#F59E0B', '#EF4444'],
        borderWidth: 2,
        borderColor: '#fff',
        hoverOffset: 6,
      }]
    },
    options: {
      cutout: '65%',
      plugins: {
        legend: { display: false },
        tooltip: {
          callbacks: {
            label: ctx => ` ${ctx.label}: ${ctx.raw}%`
          }
        }
      }
    }
  });

  document.getElementById('sentimentLegend').innerHTML = `
    <div class="sentiment-legend-item"><div class="sentiment-legend-dot dot-pos"></div>Positive: <strong>${positive}%</strong></div>
    <div class="sentiment-legend-item"><div class="sentiment-legend-dot dot-neu"></div>Neutral: <strong>${neutral}%</strong></div>
    <div class="sentiment-legend-item"><div class="sentiment-legend-dot dot-neg"></div>Negative: <strong>${negative}%</strong></div>`;
}

function renderQuickStats() {
  const ratings   = Object.values(currentUni.ratings);
  const best      = UNI_CATEGORY_META.reduce((a, b) => currentUni.ratings[a.key] > currentUni.ratings[b.key] ? a : b);
  const weakest   = UNI_CATEGORY_META.reduce((a, b) => currentUni.ratings[a.key] < currentUni.ratings[b.key] ? a : b);

  document.getElementById('quickStats').innerHTML = `
    <h3 class="heading-3 mb-3">Quick Stats</h3>
    <div style="display:flex;flex-direction:column;gap:10px">
      <div style="display:flex;justify-content:space-between;align-items:center;font-size:13px">
        <span style="color:var(--gray-400)">Highest category</span>
        <span style="font-weight:600;color:var(--success)">${best.label} (${currentUni.ratings[best.key].toFixed(1)})</span>
      </div>
      <div style="display:flex;justify-content:space-between;align-items:center;font-size:13px">
        <span style="color:var(--gray-400)">Needs improvement</span>
        <span style="font-weight:600;color:var(--warning)">${weakest.label} (${currentUni.ratings[weakest.key].toFixed(1)})</span>
      </div>
      <div style="display:flex;justify-content:space-between;align-items:center;font-size:13px">
        <span style="color:var(--gray-400)">Total reviews</span>
        <span style="font-weight:600;color:var(--primary)">${currentUni.reviewCount}</span>
      </div>
      <div style="display:flex;justify-content:space-between;align-items:center;font-size:13px">
        <span style="color:var(--gray-400)">Positive sentiment</span>
        <span style="font-weight:600;color:var(--success)">${currentUni.sentiment.positive}%</span>
      </div>
    </div>`;
}

function renderReviews(sortBy = 'recent') {
  const container = document.getElementById('reviewsList');
  container.innerHTML = '';
  let reviews = [...getUniversityReviews(currentUni.id)];

  switch (sortBy) {
    case 'highest': reviews.sort((a, b) => b.stars - a.stars); break;
    case 'lowest':  reviews.sort((a, b) => a.stars - b.stars); break;
    case 'helpful': reviews.sort((a, b) => b.helpful - a.helpful); break;
    default:        break; // already in most-recent order
  }

  if (!reviews.length) {
    container.innerHTML = `<div class="empty-state"><h4>No reviews yet</h4><p>Be the first to review this university!</p></div>`;
    return;
  }

  reviews.forEach(r => renderReviewCard(r, container));
}

function setupReviewSort() {
  document.getElementById('reviewSort').addEventListener('change', e => renderReviews(e.target.value));
}

function setupCategorySliders() {
  const container = document.getElementById('categorySliders');
  UNI_CATEGORY_META.forEach(({ key, label }) => {
    container.insertAdjacentHTML('beforeend', `
      <div class="col-6">
        <label class="er-label" style="font-size:12px">${label}</label>
        <div style="display:flex;align-items:center;gap:8px">
          <input type="range" min="1" max="5" step="1" value="3"
                 id="slider_${key}" class="form-range"
                 style="accent-color:var(--accent-blue)"
                 oninput="document.getElementById('val_${key}').textContent=this.value">
          <span id="val_${key}" style="font-weight:600;font-size:13px;min-width:12px">3</span>
        </div>
      </div>`);
  });
}

function setupReviewCharCount() {
  const ta = document.getElementById('reviewComment');
  ta.addEventListener('input', () => {
    document.getElementById('charCount').textContent = ta.value.length;
  });
}

function submitReview() {
  const comment = document.getElementById('reviewComment').value.trim();
  const errEl   = document.getElementById('commentError');
  const selected = document.querySelector('input[name="rating"]:checked');

  errEl.classList.remove('show');

  if (!selected) {
    showToast('Rating required', 'Please select a star rating.', 'error');
    return;
  }
  if (comment.length < 20) {
    errEl.classList.add('show');
    document.getElementById('reviewComment').classList.add('is-invalid');
    return;
  }

  showToast('Review Submitted!', 'Thank you! Your anonymous review has been published.');
  bootstrap.Modal.getInstance(document.getElementById('reviewModal')).hide();
  document.getElementById('reviewComment').value = '';
  document.getElementById('charCount').textContent = '0';
  document.querySelectorAll('input[name="rating"]').forEach(r => r.checked = false);
}
