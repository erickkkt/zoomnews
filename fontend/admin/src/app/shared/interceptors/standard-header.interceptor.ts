import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { DialogService } from '../services/dialog.service';

@Injectable()
export class StandardHeaderInterceptor implements HttpInterceptor {


  private cache = new Map<string, any>();

  constructor(public router: Router, private readonly _dialogService: DialogService) { }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    if (!req.headers.has('Content-Type') && !req.headers.has('Content-Disposition')) {
      req = req.clone({ headers: req.headers.set('Content-Type', 'application/json') });
    }

    req = req.clone({ headers: req.headers.set('Accept', 'application/json') });

    //Shouln't do cache because it will not load data after update object on SPA
    // if (req.method !== 'GET') {
    //   return next.handle(req).pipe(
    //     tap(
    //       event => this.handleResponse(req, event),
    //       error => this.handleError(req, error)
    //     )
    //   );
    // }

    // const cachedResponse = this.cache.get(req.url);
    // if (cachedResponse) {
    //   return of(cachedResponse);
    // }

    return next.handle(req).pipe(
      tap(
        event => this.handleResponse(req, event),
        error => this.handleError(req, error)
      )
    );
  }

  handleResponse(req: HttpRequest<any>, event) {
    //Handle response success
    if (event instanceof HttpResponse) {
       //Shouln't do cache because it will not load data after update object on SPA
      // if (req.method === 'GET') {
      //   this.cache.set(req.url, event);
      // }
    }
  }

  handleError(req: HttpRequest<any>, event) {
    const error = event.error;
    if (error) {
      const errorCode = error.Status ? error.Status : event.status;
      const exceptionId = error.Id;
      const message = error.Title

      if (event.status === 409) {
        let conflictMessage = 'The conflict inserted data! Do you want to override it?';
        const primaryButtons = 'OK';

        this._dialogService.openConfirmationDialog({ message: error ? error : conflictMessage, primaryButtons: primaryButtons }).subscribe(
          async (result) => {
            if (result) {
              //TODO: do click yes
            }
          });
      }
      else {
        this.router.navigate([`/app/error/${errorCode}/${exceptionId}/${message}`]);
      }
    }
  }
}
