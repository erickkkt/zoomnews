import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription, combineLatest } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-error-handler',
  templateUrl: './error-handler.component.html',
  styleUrls: ['./error-handler.component.scss']
})
export class ErrorHandlerComponent implements OnInit, OnDestroy {

  private paramsSubscription!: Subscription;

  errorCode: string = '';
  exceptionId: string = '';
  message: string = '';

  constructor(private readonly _activeRouter: ActivatedRoute, private readonly _router: Router) {
  }

  ngOnInit(): void {

    this.paramsSubscription = combineLatest(this._activeRouter.params,
      (params) => ({ params })).subscribe(allParams => {

        let params = allParams.params;

        this.errorCode = params['errorCode'] as string;
        this.exceptionId = params['exceptionId'] as string;
        this.message = params['message'] as string;
      });
  }


  ngOnDestroy(): void {
    this.paramsSubscription.unsubscribe();
  }

  goBack(){
    this._router.navigate(['/app'])
  }
}
