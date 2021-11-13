import {Injectable} from '@angular/core';

@Injectable()
export class LocalStorageService {

    set(key: string, value: string) {
        localStorage.setItem(key, value);
    }

    get(key: string, defaultValue: string) {
        const value = localStorage.getItem(key);
        if (value)
            return value
        else return defaultValue
    }

    remove(key: string) {
        localStorage.removeItem(key);
    }
}
