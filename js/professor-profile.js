/**
 * EduRate — Professor Profile Page
 */

const PROF_CATEGORY_META = [
  { key: 'difficulty',    label: 'Difficulty',    icon: ICONS.briefcase },
  { key: 'workload',      label: 'Workload',      icon: ICONS.building  },
  { key: 'communication', label: 'Communication', icon: ICONS.wifi      },
  { key: 'friendliness',  label: 'Friendliness',  icon: ICONS.heart     },
  { key: 'clarity',       label: 'Clarity',       icon: ICONS.smile     },
];

let currentProf   = null;
let activeFilter  = 'all';

document.addEventListener('DOMContentLoaded', () => {
  document.getElementById('navbar-placeholder').innerHTML = buildNavbar('professors');
  document.getElementById('footer-placeholder').innerHTML  = buildFooter();

  const id = getParam('id');
  currentProf = getProfessorById(id);

  if (!currentProf) {
    document.getElementById('profName').textContent = 'Professor not found';
    return;
  }

  document.title = `${currentProf.name} — EduRate`;
  renderHeader();
  renderSidebar();
  renderCategoryBars();
  renderSentimentChart();
  renderGradeChart();
  renderCoursePills();
  renderReviews();
  setupReviewSort();
  setupCategorySliders();
  setupCharCount();
  setupCourseSelect();

  document.getElementById('compareBtn').href  = `compare-professors.html?a=${currentProf.id}`;
  document.getElementById('compareBtn2').href = `compare-professors.html?a=${currentProf.id}`;
});

function renderHeader() {
  document.getElementById('breadcrumbName').textContent = currentProf.lastName;
  document.getElementById('profName').textContent = currentProf.name;

  document.getElementById('profMeta').innerHTML = `
    <span>
      <svg width="13" height="13" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M22 10v6M2 10l10-5 10 5-10 5z"/><path d="M6 12v5c3 3 9 3 12 0v-5"/></svg>
      ${currentProf.department}
    </span>
    <span>${currentProf.universityName}</span>
    <span>${starsHTML(currentProf.overallRating, 14)}</span>
    <span style="font-weight:700;color:var(--primary)">${currentProf.overallRating.toFixed(1)}</span>
    <span>${currentProf.reviewCount} reviews</span>`;
}

function renderSidebar() {
  const initials = currentProf.firstName.charAt(0) + currentProf.lastName.charAt(0);
  document.getElementById('profAvatar').textContent    = initials;
  document.getElementById('profNameCard').textContent  = currentProf.name;
  document.getElementById('profTitle').textContent     = currentProf.title;
  document.getElementById('profUni').textContent       = currentProf.universityName;
  document.getElementById('overallScore').innerHTML    = `${currentProf.overallRating.toFixed(1)}<span class="score-outof">/5</span>`;
  document.getElementById('overallStars').innerHTML    = starsHTML(currentProf.overallRating, 16);
  document.getElementById('reviewCount').textContent   = `${currentProf.reviewCount} reviews`;
  document.getElementById('wtaPercent').textContent    = `${currentProf.wouldTakeAgain}%`;

  document.getElementById('coursesCard').innerHTML = `
    <h3 class="heading-3 mb-3">Courses Taught</h3>
    <div style="display:flex;flex-direction:column;gap:6px">
      ${currentProf.courses.map(c => `
        <div style="display:flex;align-items:center;gap:8px;font-size:13px;color:var(--gray-700)">
          <svg width="13" height="13" viewBox="0 0 24 24" fill="none" stroke="var(--accent-blue)" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M4 19.5A2.5 2.5 0 0 1 6.5 17H20"/><path d="M6.5 2H20v20H6.5A2.5 2.5 0 0 1 4 19.5v-15A2.5 2.5 0 0 1 6.5 2z"/></svg>
          ${c}
        </div>`).join('')}
    </div>`;
}

function renderCategoryBars() {
  const container = document.getElementById('categoryBars');
  container.innerHTML = '';
  PROF_CATEGORY_META.forEach(({ key, label, icon }) => {
    const score = currentProf.ratings[key];
    const pct   = (score / 5) * 100;
    const color = key === 'difficulty' || key === 'workload' ? 'medium' : ratingBarColor(score);
    container.insertAdjacentHTML('beforeend', `
      <div class="rating-bar-row" style="margin-right:16px">
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

function renderSentimentChart() {
  const { positive, neutral, negative } = currentProf.sentiment;
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
        tooltip: { callbacks: { label: ctx => ` ${ctx.label}: ${ctx.raw}%` } }
      }
    }
  });

  document.getElementById('sentimentLegend').innerHTML = `
    <div class="sentiment-legend-item"><div class="sentiment-legend-dot dot-pos"></div>Positive: <strong>${positive}%</strong></div>
    <div class="sentiment-legend-item"><div class="sentiment-legend-dot dot-neu"></div>Neutral: <strong>${neutral}%</strong></div>
    <div class="sentiment-legend-item"><div class="sentiment-legend-dot dot-neg"></div>Negative: <strong>${negative}%</strong></div>`;
}

function renderGradeChart() {
  const ctx    = document.getElementById('gradeChart').getContext('2d');
  const grades = currentProf.gradeDistribution;
  const labels = Object.keys(grades);
  const values = Object.values(grades);
  const colors = ['#10B981', '#3B82F6', '#F59E0B', '#F97316', '#EF4444'];

  new Chart(ctx, {
    type: 'bar',
    data: {
      labels,
      datasets: [{
        label: '% of Students',
        data: values,
        backgroundColor: colors,
        borderRadius: 6,
        borderSkipped: false,
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: { display: false },
        tooltip: { callbacks: { label: ctx => ` ${ctx.raw}% of students` } }
      },
      scales: {
        y: {
          beginAtZero: true,
          max: 50,
          ticks: { callback: v => v + '%', font: { size: 11 } },
          grid: { color: '#F3F4F6' }
        },
        x: { ticks: { font: { size: 12, weight: '600' } }, grid: { display: false } }
      }
    }
  });
}

function renderCoursePills() {
  const container = document.getElementById('coursePills');
  const pills = [{ id: 'all', label: 'All Courses' }, ...currentProf.courses.map(c => {
    const parts = c.split(' — ');
    return { id: parts[0], label: parts[0] };
  })];

  pills.forEach(pill => {
    const btn = document.createElement('button');
    btn.className = `cat-pill${pill.id === 'all' ? ' active' : ''}`;
    btn.textContent = pill.label;
    btn.style.cssText = 'height:32px;padding:0 12px;font-size:12px';
    btn.addEventListener('click', () => {
      activeFilter = pill.id;
      container.querySelectorAll('.cat-pill').forEach(p => p.classList.remove('active'));
      btn.classList.add('active');
      renderReviews(document.getElementById('reviewSort').value);
    });
    container.appendChild(btn);
  });
}

function renderReviews(sortBy = 'recent') {
  const container = document.getElementById('reviewsList');
  container.innerHTML = '';
  let reviews = [...getProfessorReviews(currentProf.id)];

  if (activeFilter !== 'all') {
    reviews = reviews.filter(r => r.course && r.course.startsWith(activeFilter));
  }

  switch (sortBy) {
    case 'highest': reviews.sort((a, b) => b.stars - a.stars); break;
    case 'lowest':  reviews.sort((a, b) => a.stars - b.stars); break;
    case 'helpful': reviews.sort((a, b) => b.helpful - a.helpful); break;
  }

  if (!reviews.length) {
    container.innerHTML = `<div class="empty-state"><h4>No reviews for this filter</h4><p>Try selecting a different course</p></div>`;
    return;
  }
  reviews.forEach(r => renderReviewCard(r, container, true));
}

function setupReviewSort() {
  document.getElementById('reviewSort').addEventListener('change', e => renderReviews(e.target.value));
}

function setupCategorySliders() {
  const container = document.getElementById('profCategorySliders');
  PROF_CATEGORY_META.forEach(({ key, label }) => {
    container.insertAdjacentHTML('beforeend', `
      <div class="col-6">
        <label class="er-label" style="font-size:12px">${label}</label>
        <div style="display:flex;align-items:center;gap:8px">
          <input type="range" min="1" max="5" step="1" value="3"
                 id="slider_${key}" class="form-range" style="accent-color:var(--accent-blue)"
                 oninput="document.getElementById('pval_${key}').textContent=this.value">
          <span id="pval_${key}" style="font-weight:600;font-size:13px;min-width:12px">3</span>
        </div>
      </div>`);
  });
}

function setupCharCount() {
  const ta = document.getElementById('reviewComment');
  ta.addEventListener('input', () => {
    document.getElementById('charCount').textContent = ta.value.length;
  });
}

function setupCourseSelect() {
  const sel = document.getElementById('courseSelect');
  sel.innerHTML = '<option value="">Select a course</option>';
  currentProf.courses.forEach(c => {
    const opt = document.createElement('option');
    opt.value = opt.textContent = c;
    sel.appendChild(opt);
  });
}

let wtaValue = 'yes';
function setWTA(val) {
  wtaValue = val;
  document.getElementById('wtaYes').classList.toggle('active', val === 'yes');
  document.getElementById('wtaNo').classList.toggle('active',  val === 'no');
}

function submitProfReview() {
  const comment  = document.getElementById('reviewComment').value.trim();
  const errEl    = document.getElementById('commentError');
  const selected = document.querySelector('input[name="rating"]:checked');
  const course   = document.getElementById('courseSelect').value;

  errEl.classList.remove('show');

  if (!course) { showToast('Course Required', 'Please select a course.', 'error'); return; }
  if (!selected) { showToast('Rating Required', 'Please select a star rating.', 'error'); return; }
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
