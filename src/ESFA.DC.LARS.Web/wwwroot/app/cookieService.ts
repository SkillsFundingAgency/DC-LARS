export default class CookieService {
    setCookie(name: string, val: string, days: number) {
        const date = new Date();
        const value = val;

        date.setTime(date.getTime() + 24 * 60 * 60 * 1000 * days);
        document.cookie = name + "=" + value + ";path=/;expires=" + date.toUTCString();
    }

    getCookie(name: string) {
        const value = document.cookie.match('(^|;) ?' + name + '=([^;]*)(;|$)');
        return value ? value[2] : null;
    }

    deleteCookie(name: string) {
        this.setCookie(name, '', -1);
    }
}