const I18N = {
  current: 'en',

  t(key) {
    return (TRANSLATIONS[this.current] || TRANSLATIONS['en'])[key] || key;
  },

  applyTranslations() {
    document.querySelectorAll('[data-i18n]').forEach(el => {
      const key = el.getAttribute('data-i18n');
      const val = this.t(key);
      if (el.tagName === 'INPUT' || el.tagName === 'TEXTAREA') {
        el.placeholder = val;
      } else {
        el.textContent = val;
      }
    });
    document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
      el.placeholder = this.t(el.getAttribute('data-i18n-placeholder'));
    });
  },

  setLanguage(lang) {
    this.current = lang;
    localStorage.setItem('edurate_lang', lang);
    const html = document.documentElement;
    html.lang = lang;
    html.dir = lang === 'ar' ? 'rtl' : 'ltr';

    const bsRtl = document.getElementById('bs-rtl');
    if (bsRtl) bsRtl.disabled = (lang !== 'ar');

    this.applyTranslations();

    // Rebuild dynamic navbar/footer so JS-generated labels also update
    if (typeof buildNavbar === 'function') {
      const nav = document.getElementById('navbar-placeholder');
      if (nav) { nav.innerHTML = ''; buildNavbar(window._activePage || ''); }
    }
    if (typeof buildFooter === 'function') {
      const ft = document.getElementById('footer-placeholder');
      if (ft) { ft.innerHTML = ''; buildFooter(); }
    }
  },

  init() {
    const saved = localStorage.getItem('edurate_lang') || 'en';
    this.setLanguage(saved);
  }
};

function t(key) { return I18N.t(key); }

// Set saved language immediately so t() uses the correct locale before DOMContentLoaded
(function() {
  const lang = localStorage.getItem('edurate_lang') || 'en';
  I18N.current = lang;
  document.documentElement.lang = lang;
  document.documentElement.dir = lang === 'ar' ? 'rtl' : 'ltr';
})();
