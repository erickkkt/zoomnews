import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class HttpBaseService {

  constructor(private readonly http: HttpClient) { }

  public setHeaders() {
    let headers = new HttpHeaders();

    headers.append('Content-Type', 'application/json');
    headers.append('Accept', 'application/json');
    headers.append('Cache-Control', 'no-cache');
    // headers.append('Pragma', 'no-cache');
    return headers;
  }

  public getData<T>(url: string): Observable<T> {
    return this.http.get<T>(url);
  }

  public async getDataAsync<T>(url: string) {
    return await this.http.get<T>(url).toPromise();
  }

  public async postDataAsync<T>(url: string, data: any) {
    const headers = this.setHeaders();
    const httpOptions = {
      headers: headers
    };

    return await this.http.post<T>(url, data, httpOptions).toPromise();
  }

  public async putDataAsync<T>(url: string, data: any) {
    const headers = this.setHeaders();
    const httpOptions = {
      headers: headers
    };

    return await this.http.put<T>(url, data, httpOptions).toPromise();
  }

  public async deleteDataAsync<T>(url: string) {
    const headers = this.setHeaders();
    const httpOptions = {
      headers: headers
    };

    return await this.http.delete<T>(url, httpOptions).toPromise();
  }

  public upload(url: string, upload: File): Observable<object> {
    // create multipart form for file
    let formData: FormData = new FormData();
    formData.append('upload', upload, upload.name);

    const headers = new HttpHeaders().append('Content-Disposition', 'mutipart/form-data');

    // POST
    return this.http
      .post(url, formData, { headers: headers })
      .pipe(map(response => response));
  }

  public async uploadAsync<T>(url: string, upload: File) {
    // create multipart form for file
    let formData: FormData = new FormData();
    formData.append('upload', upload, upload.name);

    const headers = new HttpHeaders().append('Content-Disposition', 'mutipart/form-data');

    // POST
    return await this.http
      .post<T>(url, formData, { headers: headers }).toPromise();
  }

  public createResource(apiUrl: string, body: any): Observable<StrongType<string, string>> {
    const jsonstring = JSON.stringify(body);

    return this.http.post(
      apiUrl,
      jsonstring,
      { observe: 'response' }
    ).pipe(
      map(response => {
        return this.extractPolicyHoldersLocationHeaderGuuid(response.headers);
      })
    );
  }


  private extractPolicyHoldersLocationHeaderGuuid<NominalType extends StrongType<string, string>>(headers: HttpHeaders) {
    const locationHeader = headers.get('Location');
    if (locationHeader) {
      const matchResults = locationHeader.match(/.*\/(.+)$/);
      if (matchResults && matchResults.length > 1 && matchResults[1].length > 0) {
        return matchResults[1] as NominalType;
      }
    }
    throw new Error('Location header not found on response or header did not contain a guuid');
  }
}

export type StrongType<Type, Name extends string> = Type & { __brand: Name };
