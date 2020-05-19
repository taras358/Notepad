import { Injectable } from '@angular/core';;
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { DebtorsResponse } from '../models/deptors-response.model';
import { Observable } from 'rxjs';
import { DebtorRequest as CreateDebtorRequest } from '../models/debtor-request.model';
import { DebtorResponse } from '../models/debtor-response.model';
import { UpdateDebtorRequest } from '../models/update-debtor-request';

@Injectable({
    providedIn: 'root'
})
export class DebtorService {
    private readonly apiUrl: string;
    constructor(private http: HttpClient) {
        this.apiUrl = environment.apiURL;
    }

    public findDebtors(query: string): Observable<DebtorsResponse> {
        const params = new HttpParams().set('query', query);
        return this.http.get<DebtorsResponse>(this.apiUrl + 'api/debtor/find', {
            params: params
        });
    }
    public getGebtors(): Observable<DebtorsResponse> {
        return this.http.get<DebtorsResponse>(this.apiUrl + 'api/debtor/getDebtors');
    }
    public createDebtor(debtor: CreateDebtorRequest): Observable<CreateDebtorRequest> {
        const body = JSON.stringify(debtor);
        const headers = new HttpHeaders()
            .set('Content-Type', 'application/json; charset=utf-8');
        return this.http.post<CreateDebtorRequest>(this.apiUrl + 'api/debtor/create', body, { headers: headers, responseType: 'json' });
    }
    public getGebtor(debtorId: string) {
        const params = new HttpParams().set('debtorId', debtorId);
        return this.http.get<DebtorResponse>(this.apiUrl + 'api/debtor/getDebtor', {
            params: params
        });
    }
    public deleteDebtor(debtorId: string): Observable<void> {
        const params = new HttpParams().set('debtorId', debtorId);
        return this.http.delete<void>(this.apiUrl + 'api/debtor/delete', {
            params: params
        });
    }
    public updateDebtor(updatedDebtor: UpdateDebtorRequest): Observable<void> {
        const body = JSON.stringify(updatedDebtor);
        const headers = new HttpHeaders()
            .set('Content-Type', 'application/json; charset=utf-8');
        return this.http.patch<void>(this.apiUrl + 'api/debtor/update', body, { headers: headers, responseType: 'json' });
    }
}
