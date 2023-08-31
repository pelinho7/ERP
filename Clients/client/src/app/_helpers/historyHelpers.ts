export function getTimezone(){
  var offset = new Date().getTimezoneOffset();
  return offset/-60
}

export function getUsersLocale(): string {
  var defaultValue: string='us-US'
  if (typeof window === 'undefined' || typeof window.navigator === 'undefined') {
    return defaultValue;
  }
  const wn = window.navigator as any;
  let lang = wn.languages ? wn.languages[0] : defaultValue;
  lang = lang || wn.language || wn.browserLanguage || wn.userLanguage;
  return lang;
}