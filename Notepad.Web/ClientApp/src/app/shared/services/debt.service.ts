import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CreateDebtRequest } from '../models/create-debt-request';
import { Observable } from 'rxjs';
import { DeleteDebtRequest } from '../models/delete-debt-request';
import { UpdateDebtRequest } from '../models/update-debt-request';

@Injectable({
    providedIn: 'root'
})
export class DebtService {

    private readonly apiUrl: string;


    constructor(private http: HttpClient) {
        this.apiUrl = environment.apiURL;
    }
    public createDebt(debt: CreateDebtRequest): Observable<CreateDebtRequest> {
        const body = JSON.stringify(debt);
        const headers = new HttpHeaders()
                            .set('Content-Type', 'application/json; charset=utf-8');
        return this.http.post<CreateDebtRequest>(this.apiUrl + 'api/debt/create', body, { headers: headers, responseType: 'json' });
    }
    public deleteDebt(debt: DeleteDebtRequest): Observable<DeleteDebtRequest> {
        const body = JSON.stringify(debt);
        const headers = new HttpHeaders()
                            .set('Content-Type', 'application/json; charset=utf-8');
        return this.http.post<DeleteDebtRequest>(this.apiUrl + 'api/debt/delete', body, { headers: headers, responseType: 'json' });
    }
    public updateDebt(debt: UpdateDebtRequest ): Observable<UpdateDebtRequest> {
        const body = JSON.stringify(debt);
        const headers = new HttpHeaders()
                            .set('Content-Type', 'application/json; charset=utf-8');
        return this.http.patch<UpdateDebtRequest>(this.apiUrl + 'api/debt/update', body, { headers: headers, responseType: 'json' });
    }
}