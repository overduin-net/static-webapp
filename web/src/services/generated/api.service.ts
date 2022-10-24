//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.17.0.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, from as _observableFrom, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

import * as moment from 'moment';

export const API_URL = new InjectionToken<string>('API_URL');

export class BaseService {
  constructor() {
  }

  protected transformOptions(options: any) {
    return Promise.resolve(options);
  }

  // Not implemented yet
  protected transformResult(url: string, response: Response, processor: (response: Response) => any) {
    return processor(response);
  }
}

@Injectable()
export class ApiService extends BaseService {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_URL) baseUrl?: string) {
        super();
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "http://localhost:7071";
    }

    /**
     * @param body (optional) 
     * @return Success
     */
    createPerson(body: PersonDTO | undefined): Observable<PersonDTO> {
        let url_ = this.baseUrl + "/api/v1/person/create";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
            return this.http.request("post", url_, transformedOptions_);
        })).pipe(_observableMergeMap((response_: any) => {
            return this.processCreatePerson(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCreatePerson(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<PersonDTO>;
                }
            } else
                return _observableThrow(response_) as any as Observable<PersonDTO>;
        }));
    }

    protected processCreatePerson(response: HttpResponseBase): Observable<PersonDTO> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 201) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result201: any = null;
            let resultData201 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result201 = PersonDTO.fromJS(resultData201);
            return _observableOf(result201);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<PersonDTO>(null as any);
    }

    /**
     * @return Success
     */
    getAllPersons(): Observable<PersonDTO[]> {
        let url_ = this.baseUrl + "/api/v1/person/get-all";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
            return this.http.request("get", url_, transformedOptions_);
        })).pipe(_observableMergeMap((response_: any) => {
            return this.processGetAllPersons(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAllPersons(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<PersonDTO[]>;
                }
            } else
                return _observableThrow(response_) as any as Observable<PersonDTO[]>;
        }));
    }

    protected processGetAllPersons(response: HttpResponseBase): Observable<PersonDTO[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(PersonDTO.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<PersonDTO[]>(null as any);
    }
}

export class PersonDTO implements IPersonDTO {
    id?: string | undefined;
    name?: string | undefined;
    email?: string | undefined;

    constructor(data?: IPersonDTO) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.email = _data["email"];
        }
    }

    static fromJS(data: any): PersonDTO {
        data = typeof data === 'object' ? data : {};
        let result = new PersonDTO();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["email"] = this.email;
        return data;
    }

    clone(): PersonDTO {
        const json = this.toJSON();
        let result = new PersonDTO();
        result.init(json);
        return result;
    }
}

export interface IPersonDTO {
    id?: string | undefined;
    name?: string | undefined;
    email?: string | undefined;
}

export class SwaggerException extends Error {
    override message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    return _observableThrow(new SwaggerException(message, status, response, headers, result));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}