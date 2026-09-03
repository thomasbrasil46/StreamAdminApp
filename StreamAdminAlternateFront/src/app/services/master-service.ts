import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class MasterService {
    http = inject(HttpClient);

    onRegisterUser(obj:any) {
        return this.http.post('https://localhost:7157/api/UserAccess', obj);
    }
}
