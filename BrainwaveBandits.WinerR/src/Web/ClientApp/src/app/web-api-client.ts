//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export interface IImportedWineClient {
    search(searchQuery: string | null): Observable<ImportedWineSearchResultDto[]>;
}

@Injectable({
    providedIn: 'root'
})
export class ImportedWineClient implements IImportedWineClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ?? "";
    }

    search(searchQuery: string | null): Observable<ImportedWineSearchResultDto[]> {
        let url_ = this.baseUrl + "/api/ImportedWine?";
        if (searchQuery === undefined)
            throw new Error("The parameter 'searchQuery' must be defined.");
        else if(searchQuery !== null)
            url_ += "SearchQuery=" + encodeURIComponent("" + searchQuery) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processSearch(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processSearch(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ImportedWineSearchResultDto[]>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ImportedWineSearchResultDto[]>;
        }));
    }

    protected processSearch(response: HttpResponseBase): Observable<ImportedWineSearchResultDto[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(ImportedWineSearchResultDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }
}

export interface IRecipeClient {
    getIRecipeFromDishName(dishName: string | null): Observable<Recipe>;
}

@Injectable({
    providedIn: 'root'
})
export class RecipeClient implements IRecipeClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ?? "";
    }

    getIRecipeFromDishName(dishName: string | null): Observable<Recipe> {
        let url_ = this.baseUrl + "/api/Recipe?";
        if (dishName === undefined)
            throw new Error("The parameter 'dishName' must be defined.");
        else if(dishName !== null)
            url_ += "DishName=" + encodeURIComponent("" + dishName) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetIRecipeFromDishName(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetIRecipeFromDishName(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<Recipe>;
                }
            } else
                return _observableThrow(response_) as any as Observable<Recipe>;
        }));
    }

    protected processGetIRecipeFromDishName(response: HttpResponseBase): Observable<Recipe> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = Recipe.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }
}

export interface IWinesClient {
    getWinesWithPagination(pageNumber: number, pageSize: number): Observable<PaginatedListOfWineBriefDto>;
    createWine(command: CreateWineCommand): Observable<number>;
    recommendWine(dishName: string | null): Observable<WineBriefDto[]>;
    createWineByVoice(file: FileParameter | null | undefined): Observable<number[]>;
    createOrUpdateWineByWinesIdList(command: CreateOrUpdateWinesByIdListCommand): Observable<void>;
    updateWine(id: number, command: UpdateWineCommand): Observable<void>;
    deleteWine(id: number): Observable<void>;
}

@Injectable({
    providedIn: 'root'
})
export class WinesClient implements IWinesClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ?? "";
    }

    getWinesWithPagination(pageNumber: number, pageSize: number): Observable<PaginatedListOfWineBriefDto> {
        let url_ = this.baseUrl + "/api/Wines?";
        if (pageNumber === undefined || pageNumber === null)
            throw new Error("The parameter 'pageNumber' must be defined and cannot be null.");
        else
            url_ += "PageNumber=" + encodeURIComponent("" + pageNumber) + "&";
        if (pageSize === undefined || pageSize === null)
            throw new Error("The parameter 'pageSize' must be defined and cannot be null.");
        else
            url_ += "PageSize=" + encodeURIComponent("" + pageSize) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetWinesWithPagination(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetWinesWithPagination(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<PaginatedListOfWineBriefDto>;
                }
            } else
                return _observableThrow(response_) as any as Observable<PaginatedListOfWineBriefDto>;
        }));
    }

    protected processGetWinesWithPagination(response: HttpResponseBase): Observable<PaginatedListOfWineBriefDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = PaginatedListOfWineBriefDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    createWine(command: CreateWineCommand): Observable<number> {
        let url_ = this.baseUrl + "/api/Wines";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCreateWine(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCreateWine(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<number>;
                }
            } else
                return _observableThrow(response_) as any as Observable<number>;
        }));
    }

    protected processCreateWine(response: HttpResponseBase): Observable<number> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result200 = resultData200 !== undefined ? resultData200 : <any>null;
    
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    recommendWine(dishName: string | null): Observable<WineBriefDto[]> {
        let url_ = this.baseUrl + "/api/Wines/recommend?";
        if (dishName === undefined)
            throw new Error("The parameter 'dishName' must be defined.");
        else if(dishName !== null)
            url_ += "DishName=" + encodeURIComponent("" + dishName) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processRecommendWine(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processRecommendWine(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<WineBriefDto[]>;
                }
            } else
                return _observableThrow(response_) as any as Observable<WineBriefDto[]>;
        }));
    }

    protected processRecommendWine(response: HttpResponseBase): Observable<WineBriefDto[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(WineBriefDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    createWineByVoice(file: FileParameter | null | undefined): Observable<number[]> {
        let url_ = this.baseUrl + "/api/Wines/voice";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = new FormData();
        if (file !== null && file !== undefined)
            content_.append("file", file.data, file.fileName ? file.fileName : "file");

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCreateWineByVoice(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCreateWineByVoice(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<number[]>;
                }
            } else
                return _observableThrow(response_) as any as Observable<number[]>;
        }));
    }

    protected processCreateWineByVoice(response: HttpResponseBase): Observable<number[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(item);
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    createOrUpdateWineByWinesIdList(command: CreateOrUpdateWinesByIdListCommand): Observable<void> {
        let url_ = this.baseUrl + "/api/Wines/createorupdate";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCreateOrUpdateWineByWinesIdList(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCreateOrUpdateWineByWinesIdList(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<void>;
                }
            } else
                return _observableThrow(response_) as any as Observable<void>;
        }));
    }

    protected processCreateOrUpdateWineByWinesIdList(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return _observableOf(null as any);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    updateWine(id: number, command: UpdateWineCommand): Observable<void> {
        let url_ = this.baseUrl + "/api/Wines/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("put", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUpdateWine(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUpdateWine(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<void>;
                }
            } else
                return _observableThrow(response_) as any as Observable<void>;
        }));
    }

    protected processUpdateWine(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return _observableOf(null as any);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    deleteWine(id: number): Observable<void> {
        let url_ = this.baseUrl + "/api/Wines/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("delete", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDeleteWine(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDeleteWine(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<void>;
                }
            } else
                return _observableThrow(response_) as any as Observable<void>;
        }));
    }

    protected processDeleteWine(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return _observableOf(null as any);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }
}

export class ImportedWineSearchResultDto implements IImportedWineSearchResultDto {
    wineId?: string;
    title?: string;

    constructor(data?: IImportedWineSearchResultDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.wineId = _data["wineId"];
            this.title = _data["title"];
        }
    }

    static fromJS(data: any): ImportedWineSearchResultDto {
        data = typeof data === 'object' ? data : {};
        let result = new ImportedWineSearchResultDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["wineId"] = this.wineId;
        data["title"] = this.title;
        return data;
    }
}

export interface IImportedWineSearchResultDto {
    wineId?: string;
    title?: string;
}

export class Recipe implements IRecipe {
    name?: string;
    ingredients?: Ingredient[];
    mainIngredient?: Ingredient;

    constructor(data?: IRecipe) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            if (Array.isArray(_data["ingredients"])) {
                this.ingredients = [] as any;
                for (let item of _data["ingredients"])
                    this.ingredients!.push(Ingredient.fromJS(item));
            }
            this.mainIngredient = _data["mainIngredient"] ? Ingredient.fromJS(_data["mainIngredient"]) : <any>undefined;
        }
    }

    static fromJS(data: any): Recipe {
        data = typeof data === 'object' ? data : {};
        let result = new Recipe();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        if (Array.isArray(this.ingredients)) {
            data["ingredients"] = [];
            for (let item of this.ingredients)
                data["ingredients"].push(item.toJSON());
        }
        data["mainIngredient"] = this.mainIngredient ? this.mainIngredient.toJSON() : <any>undefined;
        return data;
    }
}

export interface IRecipe {
    name?: string;
    ingredients?: Ingredient[];
    mainIngredient?: Ingredient;
}

export class Ingredient implements IIngredient {
    name?: string;

    constructor(data?: IIngredient) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): Ingredient {
        data = typeof data === 'object' ? data : {};
        let result = new Ingredient();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        return data;
    }
}

export interface IIngredient {
    name?: string;
}

export class PaginatedListOfWineBriefDto implements IPaginatedListOfWineBriefDto {
    items?: WineBriefDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;

    constructor(data?: IPaginatedListOfWineBriefDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(WineBriefDto.fromJS(item));
            }
            this.pageNumber = _data["pageNumber"];
            this.totalPages = _data["totalPages"];
            this.totalCount = _data["totalCount"];
            this.hasPreviousPage = _data["hasPreviousPage"];
            this.hasNextPage = _data["hasNextPage"];
        }
    }

    static fromJS(data: any): PaginatedListOfWineBriefDto {
        data = typeof data === 'object' ? data : {};
        let result = new PaginatedListOfWineBriefDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.items)) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        data["pageNumber"] = this.pageNumber;
        data["totalPages"] = this.totalPages;
        data["totalCount"] = this.totalCount;
        data["hasPreviousPage"] = this.hasPreviousPage;
        data["hasNextPage"] = this.hasNextPage;
        return data;
    }
}

export interface IPaginatedListOfWineBriefDto {
    items?: WineBriefDto[];
    pageNumber?: number;
    totalPages?: number;
    totalCount?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;
}

export class WineBriefDto implements IWineBriefDto {
    id?: number;
    wineId?: string;
    name?: string;
    brand?: string | undefined;
    vintage?: number;
    amount?: number;
    done?: boolean;

    constructor(data?: IWineBriefDto) {
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
            this.wineId = _data["wineId"];
            this.name = _data["name"];
            this.brand = _data["brand"];
            this.vintage = _data["vintage"];
            this.amount = _data["amount"];
            this.done = _data["done"];
        }
    }

    static fromJS(data: any): WineBriefDto {
        data = typeof data === 'object' ? data : {};
        let result = new WineBriefDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["wineId"] = this.wineId;
        data["name"] = this.name;
        data["brand"] = this.brand;
        data["vintage"] = this.vintage;
        data["amount"] = this.amount;
        data["done"] = this.done;
        return data;
    }
}

export interface IWineBriefDto {
    id?: number;
    wineId?: string;
    name?: string;
    brand?: string | undefined;
    vintage?: number;
    amount?: number;
    done?: boolean;
}

export class CreateWineCommand implements ICreateWineCommand {
    wineId?: string;
    name?: string;
    brand?: string | undefined;
    vintage?: number;
    amount?: number;

    constructor(data?: ICreateWineCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.wineId = _data["wineId"];
            this.name = _data["name"];
            this.brand = _data["brand"];
            this.vintage = _data["vintage"];
            this.amount = _data["amount"];
        }
    }

    static fromJS(data: any): CreateWineCommand {
        data = typeof data === 'object' ? data : {};
        let result = new CreateWineCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["wineId"] = this.wineId;
        data["name"] = this.name;
        data["brand"] = this.brand;
        data["vintage"] = this.vintage;
        data["amount"] = this.amount;
        return data;
    }
}

export interface ICreateWineCommand {
    wineId?: string;
    name?: string;
    brand?: string | undefined;
    vintage?: number;
    amount?: number;
}

export class CreateOrUpdateWinesByIdListCommand implements ICreateOrUpdateWinesByIdListCommand {
    wineIdList?: string[];

    constructor(data?: ICreateOrUpdateWinesByIdListCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["wineIdList"])) {
                this.wineIdList = [] as any;
                for (let item of _data["wineIdList"])
                    this.wineIdList!.push(item);
            }
        }
    }

    static fromJS(data: any): CreateOrUpdateWinesByIdListCommand {
        data = typeof data === 'object' ? data : {};
        let result = new CreateOrUpdateWinesByIdListCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.wineIdList)) {
            data["wineIdList"] = [];
            for (let item of this.wineIdList)
                data["wineIdList"].push(item);
        }
        return data;
    }
}

export interface ICreateOrUpdateWinesByIdListCommand {
    wineIdList?: string[];
}

export class UpdateWineCommand implements IUpdateWineCommand {
    id?: number;
    wineID?: string;
    name?: string;
    brand?: string | undefined;
    vintage?: number;
    amount?: number;
    done?: boolean;

    constructor(data?: IUpdateWineCommand) {
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
            this.wineID = _data["wineID"];
            this.name = _data["name"];
            this.brand = _data["brand"];
            this.vintage = _data["vintage"];
            this.amount = _data["amount"];
            this.done = _data["done"];
        }
    }

    static fromJS(data: any): UpdateWineCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateWineCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["wineID"] = this.wineID;
        data["name"] = this.name;
        data["brand"] = this.brand;
        data["vintage"] = this.vintage;
        data["amount"] = this.amount;
        data["done"] = this.done;
        return data;
    }
}

export interface IUpdateWineCommand {
    id?: number;
    wineID?: string;
    name?: string;
    brand?: string | undefined;
    vintage?: number;
    amount?: number;
    done?: boolean;
}

export interface FileParameter {
    data: any;
    fileName: string;
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
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
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