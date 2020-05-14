/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { CreateDebtorRequest } from '../models/create-debtor-request';
import { UpdateDebtorRequest } from '../models/update-debtor-request';

@Injectable({
  providedIn: 'root',
})
export class DebtorService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiDebtorFindGet
   */
  static readonly ApiDebtorFindGetPath = '/api/Debtor/find';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDebtorFindGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDebtorFindGet$Response(params?: {
    query?: null | string;

  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, DebtorService.ApiDebtorFindGetPath, 'get');
    if (params) {

      rb.query('query', params.query, {});

    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDebtorFindGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDebtorFindGet(params?: {
    query?: null | string;

  }): Observable<void> {

    return this.apiDebtorFindGet$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiDebtorGetDebtorGet
   */
  static readonly ApiDebtorGetDebtorGetPath = '/api/Debtor/getDebtor';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDebtorGetDebtorGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDebtorGetDebtorGet$Response(params?: {
    debtorId?: null | string;

  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, DebtorService.ApiDebtorGetDebtorGetPath, 'get');
    if (params) {

      rb.query('debtorId', params.debtorId, {});

    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDebtorGetDebtorGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDebtorGetDebtorGet(params?: {
    debtorId?: null | string;

  }): Observable<void> {

    return this.apiDebtorGetDebtorGet$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiDebtorGetDebtorsGet
   */
  static readonly ApiDebtorGetDebtorsGetPath = '/api/Debtor/getDebtors';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDebtorGetDebtorsGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDebtorGetDebtorsGet$Response(params?: {

  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, DebtorService.ApiDebtorGetDebtorsGetPath, 'get');
    if (params) {


    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDebtorGetDebtorsGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDebtorGetDebtorsGet(params?: {

  }): Observable<void> {

    return this.apiDebtorGetDebtorsGet$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiDebtorCreatePost
   */
  static readonly ApiDebtorCreatePostPath = '/api/Debtor/create';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDebtorCreatePost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDebtorCreatePost$Response(params?: {
      body?: CreateDebtorRequest
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, DebtorService.ApiDebtorCreatePostPath, 'post');
    if (params) {


      rb.body(params.body, 'application/*+json');
    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDebtorCreatePost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDebtorCreatePost(params?: {
      body?: CreateDebtorRequest
  }): Observable<void> {

    return this.apiDebtorCreatePost$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation apiDebtorUpdatePost
   */
  static readonly ApiDebtorUpdatePostPath = '/api/Debtor/update';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDebtorUpdatePost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDebtorUpdatePost$Response(params?: {
      body?: UpdateDebtorRequest
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, DebtorService.ApiDebtorUpdatePostPath, 'post');
    if (params) {


      rb.body(params.body, 'application/*+json');
    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDebtorUpdatePost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDebtorUpdatePost(params?: {
      body?: UpdateDebtorRequest
  }): Observable<void> {

    return this.apiDebtorUpdatePost$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
