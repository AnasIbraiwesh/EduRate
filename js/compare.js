/**
 * EduRate — Comparison Pages (shared for universities & professors)
 */

const UNI_CATS  = [
  { key: 'location',      label: 'Location'      },
  { key: 'reputation',    label: 'Reputation'    },
  { key: 'safety',        label: 'Safety'        },
  { key: 'food',          label: 'Food'          },
  { key: 'facilities',    label: 'Facilities'    },
  { key: 'clubs',         label: 'Clubs'         },
  { key: 'happiness',     label: 'Happiness'     },
  { key: 'internet',      label: 'Internet'      },
  { key: 'opportunities', label: 'Opportunities' },
  { key: 'socialLife',    label: 'Social Life'   },
];

const PROF_CATS = [
  { key: 'difficulty',    label: 'Difficulty'    },
  { key: 'workload',      label: 'Workload'      },
  { key: 'communication', label: 'Communication' },
  { key: 'friendliness',  label: 'Friendliness'  },
  { key: 'clarity',       label: 'Clarity'       },
];

let mode        = 'university';
let radarChart  = null;

function initComparePage(pageMode) {
  mode = pageMode;

  document.getElementById('navbar-placeholder').innerHTML = buildNavbar('compare');
  document.getElementById('footer-placeholder').innerHTML  = buildFooter();

  const dataset    = mode === 'university' ? UNIVERSITIES : PROFESSORS;
  const nameGetter = mode === 'university' ? u => u.name : p => p.name;

  // Populate dropdowns
  const selA = document.getElementById('selectA');
  const selB = document.getElementById('selectB');

  const blankA = document.createElement('option');
  blankA.value = ''; blankA.textContent = `-- Select ${mode === 'university' ? 'University' : 'Professor'} A --`;
  selA.appendChild(blankA);

  const blankB = document.createElement('option');
  blankB.value = ''; blankB.textContent = `-- Select ${mode === 'university' ? 'University' : 'Professor'} B --`;
  selB.appendChild(blankB);

  dataset.forEach(item => {
    const optA = document.createElement('option');
    optA.value = item.id;
    optA.textContent = mode === 'university' ? item.name : `${item.name} (${item.department})`;
    selA.appendChild(optA);

    const optB = optA.cloneNode(true);
    selB.appendChild(optB);
  });

  // Pre-fill from URL params
  const paramA = getParam('a');
  const paramB = getParam('b');
  if (paramA) selA.value = paramA;
  if (paramB) selB.value = paramB;

  selA.addEventListener('change', tryRender);
  selB.addEventListener('change', tryRender);

  if (paramA && paramB) tryRender();
  else if (paramA && !paramB) {
    // Auto-suggest a B different from A
    const other = dataset.find(d => d.id !== parseInt(paramA));
    if (other) { selB.value = other.id; tryRender(); }
  }
}

function tryRender() {
  const idA = document.getElementById('selectA').value;
  const idB = document.getElementById('selectB').value;

  if (!idA || !idB || idA === idB) {
    document.getElementById('placeholder').classList.remove('d-none');
    document.getElementById('comparisonContent').classList.add('d-none');
    return;
  }

  const dataset = mode === 'university' ? UNIVERSITIES : PROFESSORS;
  const entA    = dataset.find(d => d.id === parseInt(idA));
  const entB    = dataset.find(d => d.id === parseInt(idB));

  if (!entA || !entB) return;

  document.getElementById('placeholder').classList.add('d-none');
  document.getElementById('comparisonContent').classList.remove('d-none');

  updateURL(idA, idB);
  renderHeaders(entA, entB);
  renderRows(entA, entB);
  renderWinners(entA, entB);
  renderRadar(entA, entB);
}

function updateURL(a, b) {
  const url = new URL(window.location);
  url.searchParams.set('a', a);
  url.searchParams.set('b', b);
  window.history.replaceState({}, '', url);
}

function renderHeaders(entA, entB) {
  const isUni = mode === 'university';

  document.getElementById('headerA').innerHTML = `
    <div class="entity-name">${isUni ? entA.name : entA.name}</div>
    <div style="font-size:13px;color:var(--gray-400);margin-bottom:8px">${isUni ? entA.city : entA.department}</div>
    <div style="display:flex;align-items:center;justify-content:center;gap:6px">
      ${starsHTML(entA.overallRating)}
      <strong>${entA.overallRating.toFixed(1)}</strong>
    </div>`;

  document.getElementById('headerB').innerHTML = `
    <div class="entity-name">${isUni ? entB.name : entB.name}</div>
    <div style="font-size:13px;color:var(--gray-400);margin-bottom:8px">${isUni ? entB.city : entB.department}</div>
    <div style="display:flex;align-items:center;justify-content:center;gap:6px">
      ${starsHTML(entB.overallRating)}
      <strong>${entB.overallRating.toFixed(1)}</strong>
    </div>`;
}

function renderRows(entA, entB) {
  const cats      = mode === 'university' ? UNI_CATS : PROF_CATS;
  const container = document.getElementById('comparisonRows');
  container.innerHTML = '';

  cats.forEach(({ key, label }) => {
    const scoreA   = entA.ratings[key];
    const scoreB   = entB.ratings[key];
    const aWins    = scoreA > scoreB;
    const bWins    = scoreB > scoreA;

    container.insertAdjacentHTML('beforeend', `
      <div class="compare-row">
        <!-- A side -->
        <div class="compare-bar-col" style="flex:1;display:flex;align-items:center;gap:8px">
          <span class="score-badge ${aWins ? 'winner' : 'loser'}">${scoreA.toFixed(1)}</span>
          <div class="compare-track" style="flex:1">
            <div class="compare-fill ${aWins ? 'winner-fill' : 'loser-fill'}"
                 data-width="${(scoreA/5)*100}" style="width:0"></div>
          </div>
        </div>
        <!-- Label -->
        <div class="compare-row cat-label">${label}</div>
        <!-- B side -->
        <div class="compare-bar-col" style="flex:1;display:flex;align-items:center;gap:8px;flex-direction:row-reverse">
          <span class="score-badge ${bWins ? 'winner' : 'loser'}">${scoreB.toFixed(1)}</span>
          <div class="compare-track" style="flex:1">
            <div class="compare-fill ${bWins ? 'winner-fill' : 'loser-fill'}"
                 data-width="${(scoreB/5)*100}" style="width:0"></div>
          </div>
        </div>
      </div>`);
  });

  // Animate bars
  setTimeout(() => {
    container.querySelectorAll('.compare-fill[data-width]').forEach(bar => {
      bar.style.width = bar.dataset.width + '%';
    });
  }, 150);
}

function renderWinners(entA, entB) {
  const cats   = mode === 'university' ? UNI_CATS : PROF_CATS;
  const aWins  = cats.filter(c => entA.ratings[c.key] > entB.ratings[c.key]).length;
  const bWins  = cats.filter(c => entB.ratings[c.key] > entA.ratings[c.key]).length;
  const isUni  = mode === 'university';

  const overallA = entA.overallRating;
  const overallB = entB.overallRating;
  const aIsBetter = overallA >= overallB;

  document.getElementById('winnerA').innerHTML = `
    <div style="font-family:var(--font-heading);font-weight:700;font-size:16px;color:var(--primary);margin-bottom:6px">${entA.name}</div>
    <div class="winner-badge" style="display:inline-flex;margin-bottom:8px">
      ${ICONS.trophy} Higher in ${aWins} categor${aWins === 1 ? 'y' : 'ies'}
    </div>
    ${aIsBetter ? `<div style="background:rgba(16,185,129,0.08);border-radius:var(--radius-md);padding:8px 14px;display:inline-block">
      <span style="font-size:12px;font-weight:700;color:var(--success)">🏆 Recommended</span>
    </div>` : ''}`;

  document.getElementById('winnerB').innerHTML = `
    <div style="font-family:var(--font-heading);font-weight:700;font-size:16px;color:var(--primary);margin-bottom:6px">${entB.name}</div>
    <div class="winner-badge" style="display:inline-flex;margin-bottom:8px">
      ${ICONS.trophy} Higher in ${bWins} categor${bWins === 1 ? 'y' : 'ies'}
    </div>
    ${!aIsBetter ? `<div style="background:rgba(16,185,129,0.08);border-radius:var(--radius-md);padding:8px 14px;display:inline-block">
      <span style="font-size:12px;font-weight:700;color:var(--success)">🏆 Recommended</span>
    </div>` : ''}`;
}

function renderRadar(entA, entB) {
  const cats   = mode === 'university' ? UNI_CATS : PROF_CATS;
  const labels = cats.map(c => c.label);
  const dataA  = cats.map(c => entA.ratings[c.key]);
  const dataB  = cats.map(c => entB.ratings[c.key]);
  const nameA  = mode === 'university' ? entA.shortName || entA.name.split(' ')[0] : entA.lastName;
  const nameB  = mode === 'university' ? entB.shortName || entB.name.split(' ')[0] : entB.lastName;

  if (radarChart) { radarChart.destroy(); radarChart = null; }

  const ctx = document.getElementById('radarChart').getContext('2d');
  radarChart = new Chart(ctx, {
    type: 'radar',
    data: {
      labels,
      datasets: [
        {
          label: entA.name,
          data: dataA,
          backgroundColor: 'rgba(59,130,246,0.15)',
          borderColor: '#3B82F6',
          borderWidth: 2,
          pointBackgroundColor: '#3B82F6',
          pointRadius: 4,
        },
        {
          label: entB.name,
          data: dataB,
          backgroundColor: 'rgba(16,185,129,0.15)',
          borderColor: '#10B981',
          borderWidth: 2,
          pointBackgroundColor: '#10B981',
          pointRadius: 4,
        }
      ]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        r: {
          min: 0,
          max: 5,
          ticks: { stepSize: 1, font: { size: 10 }, backdropColor: 'transparent' },
          grid: { color: '#E5E7EB' },
          angleLines: { color: '#E5E7EB' },
          pointLabels: { font: { size: 11, family: 'Inter' }, color: '#374151' }
        }
      },
      plugins: {
        legend: { display: false }
      }
    }
  });

  document.getElementById('radarLegend').innerHTML = `
    <div style="display:flex;align-items:center;gap:8px;font-size:13px;color:var(--gray-700)">
      <div style="width:24px;height:3px;background:#3B82F6;border-radius:2px"></div>
      ${entA.name}
    </div>
    <div style="display:flex;align-items:center;gap:8px;font-size:13px;color:var(--gray-700)">
      <div style="width:24px;height:3px;background:#10B981;border-radius:2px"></div>
      ${entB.name}
    </div>`;
}

function shareComparison() {
  const url = window.location.href;
  if (navigator.clipboard) {
    navigator.clipboard.writeText(url).then(() => {
      showToast('Link Copied!', 'Comparison link copied to clipboard.');
    });
  } else {
    const el = document.createElement('textarea');
    el.value = url;
    document.body.appendChild(el);
    el.select();
    document.execCommand('copy');
    document.body.removeChild(el);
    showToast('Link Copied!', 'Comparison link copied to clipboard.');
  }
}
