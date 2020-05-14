/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { CreateUserRequest } from '../models/create-user-request';
import { JwtAuthResponse } from '../models/jwt-auth-response';
import { LoginRequest } from '../models/login-request';
import { RefreshTokenRequest } from '../models/refresh-token-request';
import { UserResponse } from '../models/user-response';
import { UsersResponse } from '../models/users-response';

@Injectable({
  providedIn: 'root',
})
export class AccountService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiAccountLoginPost
   */
  static readonly ApiAccountLoginPostPath = '/api/Account/Login';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountLoginPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountLoginPost$Response(params?: {
      body?: LoginRequest
  }): Observable<StrictHttpResponse<JwtAuthResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountLoginPostPath, 'post');
    if (params) {


      rb.body(params.body, 'application/*+json');
    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<JwtAuthResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountLoginPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountLoginPost(params?: {
      body?: LoginRequest
  }): Observable<JwtAuthResponse> {

    return this.apiAccountLoginPost$Response(params).pipe(
      map((r: StrictHttpResponse<JwtAuthResponse>) => r.body as JwtAuthResponse)
    );
  }

  /**
   * Path part for operation apiAccountRefreshTokenPost
   */
  static readonly ApiAccountRefreshTokenPostPath = '/api/Account/RefreshToken';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountRefreshTokenPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRefreshTokenPost$Response(params?: {
      body?: RefreshTokenRequest
  }): Observable<StrictHttpResponse<JwtAuthResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountRefreshTokenPostPath, 'post');
    if (params) {


      rb.body(params.body, 'application/*+json');
    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<JwtAuthResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountRefreshTokenPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRefreshTokenPost(params?: {
      body?: RefreshTokenRequest
  }): Observable<JwtAuthResponse> {

    return this.apiAccountRefreshTokenPost$Response(params).pipe(
      map((r: StrictHttpResponse<JwtAuthResponse>) => r.body as JwtAuthResponse)
    );
  }

  /**
   * Path part for operation apiAccountCreatePost
   */
  static readonly ApiAccountCreatePostPath = '/api/Account/Create';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountCreatePost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountCreatePost$Response(params?: {
      body?: CreateUserRequest
  }): Observable<StrictHttpResponse<UserResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountCreatePostPath, 'post');
    if (params) {


      rb.body(params.body, 'application/*+json');
    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountCreatePost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountCreatePost(params?: {
      body?: CreateUserRequest
  }): Observable<UserResponse> {

    return this.apiAccountCreatePost$Response(params).pipe(
      map((r: StrictHttpResponse<UserResponse>) => r.body as UserResponse)
    );
  }

  /**
   * Path part for operation apiAccountGetByIdGet
   */
  static readonly ApiAccountGetByIdGetPath = '/api/Account/GetById';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountGetByIdGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountGetByIdGet$Response(params?: {
    id?: null | string;

  }): Observable<StrictHttpResponse<UserResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountGetByIdGetPath, 'get');
    if (params) {

      rb.query('id', params.id, {});

    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountGetByIdGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountGetByIdGet(params?: {
    id?: null | string;

  }): Observable<UserResponse> {

    return this.apiAccountGetByIdGet$Response(params).pipe(
      map((r: StrictHttpResponse<UserResponse>) => r.body as UserResponse)
    );
  }

  /**
   * Path part for operation apiAccountGetAllGet
   */
  static readonly ApiAccountGetAllGetPath = '/api/Account/GetAll';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountGetAllGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountGetAllGet$Response(params?: {

  }): Observable<StrictHttpResponse<UsersResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountGetAllGetPath, 'get');
    if (params) {


    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UsersResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountGetAllGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountGetAllGet(params?: {

  }): Observable<UsersResponse> {

    return this.apiAccountGetAllGet$Response(params).pipe(
      map((r: StrictHttpResponse<UsersResponse>) => r.body as UsersResponse)
    );
  }

  /**
   * Path part for operation apiAccountDeleteDelete
   */
  static readonly ApiAccountDeleteDeletePath = '/api/Account/Delete';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountDeleteDelete()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountDeleteDelete$Response(params?: {
    id?: null | string;

  }): Observable<StrictHttpResponse<boolean>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountDeleteDeletePath, 'delete');
    if (params) {

      rb.query('id', params.id, {});

    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: String((r as HttpResponse<any>).body) === 'true' }) as StrictHttpResponse<boolean>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountDeleteDelete$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountDeleteDelete(params?: {
    id?: null | string;

  }): Observable<boolean> {

    return this.apiAccountDeleteDelete$Response(params).pipe(
      map((r: StrictHttpResponse<boolean>) => r.body as boolean)
    );
  }

}
