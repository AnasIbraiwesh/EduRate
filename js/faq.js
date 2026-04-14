/**
 * EduRate — FAQ Page
 */

let activeCat   = 'all';
let searchQuery = '';

document.addEventListener('DOMContentLoaded', () => {
  document.getElementById('navbar-placeholder').innerHTML = buildNavbar('faq');
  document.getElementById('footer-placeholder').innerHTML  = buildFooter();

  renderFAQ();

  document.getElementById('faqSearch').addEventListener('input', e => {
    searchQuery = e.target.value.toLowerCase().trim();
    renderFAQ();
  });
});

function filterCat(btn, cat) {
  activeCat = cat;
  document.querySelectorAll('.cat-pill[data-cat]').forEach(p => p.classList.remove('active'));
  btn.classList.add('active');
  renderFAQ();
}

function renderFAQ() {
  const list  = document.getElementById('faqList');
  const empty = document.getElementById('faqEmpty');
  list.innerHTML = '';

  const filtered = FAQ_DATA.filter(item => {
    const matchCat    = activeCat === 'all' || item.category === activeCat;
    const matchSearch = !searchQuery ||
      item.question.toLowerCase().includes(searchQuery) ||
      item.answer.toLowerCase().includes(searchQuery);
    return matchCat && matchSearch;
  });

  if (!filtered.length) {
    empty.classList.remove('d-none');
    return;
  }
  empty.classList.add('d-none');

  // Group by category if "all" selected and no search
  if (activeCat === 'all' && !searchQuery) {
    const categories = [
      { id: 'general',   label: 'General' },
      { id: 'reviews',   label: 'Reviews' },
      { id: 'comparing', label: 'Comparing' },
      { id: 'account',   label: 'Account' },
      { id: 'privacy',   label: 'Privacy' },
    ];

    categories.forEach(cat => {
      const items = filtered.filter(f => f.category === cat.id);
      if (!items.length) return;

      list.insertAdjacentHTML('beforeend', `
        <h2 style="font-family:var(--font-heading);font-weight:600;font-size:16px;color:var(--primary);margin:32px 0 12px;padding-left:4px">${cat.label}</h2>`);
      items.forEach(item => renderItem(item, list));
    });
  } else {
    filtered.forEach(item => renderItem(item, list));
  }
}

function renderItem(item, container) {
  const div = document.createElement('div');
  div.className = 'faq-item';
  div.setAttribute('role', 'region');
  div.setAttribute('aria-label', item.question);

  div.innerHTML = `
    <div class="faq-question" tabindex="0" role="button"
         aria-expanded="false" aria-controls="faq-answer-${item.id}"
         onclick="toggleFAQ(this)" onkeydown="if(event.key==='Enter'||event.key===' ')toggleFAQ(this)">
      <span>${highlightMatch(item.question)}</span>
      <svg class="faq-chevron" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true">
        <polyline points="6 9 12 15 18 9"/>
      </svg>
    </div>
    <div class="faq-answer" id="faq-answer-${item.id}" role="region">
      <p>${highlightMatch(item.answer)}</p>
    </div>`;

  container.appendChild(div);
}

function toggleFAQ(btn) {
  const item     = btn.closest('.faq-item');
  const isOpen   = item.classList.contains('open');

  // Close all others
  document.querySelectorAll('.faq-item.open').forEach(el => {
    if (el !== item) {
      el.classList.remove('open');
      el.querySelector('.faq-question').setAttribute('aria-expanded', 'false');
    }
  });

  item.classList.toggle('open', !isOpen);
  btn.setAttribute('aria-expanded', String(!isOpen));
}

function highlightMatch(text) {
  if (!searchQuery) return escapeHTML(text);
  const escaped = escapeHTML(text);
  const escapedQ = escapeHTML(searchQuery);
  const regex = new RegExp(`(${escapedQ.replace(/[.*+?^${}()|[\]\\]/g, '\\$&')})`, 'gi');
  return escaped.replace(regex, '<mark style="background:rgba(59,130,246,0.2);border-radius:2px;padding:0 2px">$1</mark>');
}
