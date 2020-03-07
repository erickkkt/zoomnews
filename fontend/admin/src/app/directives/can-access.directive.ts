import { Directive, OnInit, OnDestroy, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthorizationService } from '../services/authorization.service';

@Directive({
  selector: '[appCanAccess]'
})
export class CanAccessDirective implements OnInit, OnDestroy {
  @Input('appCanAccess') appCanAcess: string;
  private permission$: Subscription;

  constructor(private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private authorizationService: AuthorizationService) { }

  private applyPermission() {
    this.permission$ = this.authorizationService.checkAuthorization(this.appCanAcess).subscribe(authorized => {
      if (authorized) {
        this.viewContainer.createEmbeddedView(this.templateRef);
      }
      else {
        this.viewContainer.clear();
      }
    });
  }
  ngOnInit(): void {
    this.applyPermission();
  }
  ngOnDestroy(): void {
    this.permission$.unsubscribe();
  }



}
