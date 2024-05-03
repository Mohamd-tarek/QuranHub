import { Injectable } from '@angular/core';

@Injectable()
export class SessionStorageService {

    set(key: string, value: string) {
        sessionStorage.setItem(key, value);
    }

    setJSON(key: string, value: Object) {
        sessionStorage.setItem(key, JSON.stringify(value));
    }

    get(key: string){
        return sessionStorage.getItem(key);
    }

    remove(key: string) {
        sessionStorage.removeItem(key);
    }

    clear(){
        sessionStorage.clear();
    }
}
