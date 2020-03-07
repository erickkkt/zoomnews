import { Injectable } from '@angular/core';
import { HttpBaseService } from '../shared/services/http-base.service';
import { ApiEndPoints } from '../shared/config/ApiEndPoints';
import { Observable, of, ReplaySubject } from 'rxjs';
import { ActionRoles } from '../models/action-roles';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  private actionRoles: ActionRoles[] = [];
  private roles: string[] = [];

  private actionRolesSubject = new ReplaySubject<ActionRoles[]>(1);
  private hasActionRoles = false;
  constructor(private httpService: HttpBaseService, private api: ApiEndPoints, private authService: AuthService) {
    // this.authService.isAuthenticated$.subscribe(isauthenticated => {
    //   if (isauthenticated) {
    //     this.getActionRoles();
    //   }
    // });
    // this.getactionroles();
    // console.log('realy sync!');

    // this.getActionRoles();
    // const claims = this.authService.identityClaims as any;
    // this.roles = claims.role;
  }

  public checkAuthorization(action: string): Observable<boolean> {
   
    if (this.actionRoles.length > 0) {
      return of(this.checkRoles(this.actionRoles, this.roles, action));
    }

    
    //  else {
    //   return this.getActionRoles().pipe(
    //     map(r => {
    //       this.actionRoles = r;
    //       return this.checkRoles(this.actionRoles, this.roles, action)
    //     })
    //   );
    // }
  }

  private checkRoles(actionRoles: ActionRoles[], claims: string[], action: string): boolean {
    let hasPermission = false;
    const actionRole = actionRoles.find(x => x.action == action);
    if (!actionRole) {
      return false;
    }
    claims.forEach(role => {
      const result = actionRole.roles.find(x => x === role);
      if (result && result.length > 0) {
        hasPermission = true;
      }
    });
    return hasPermission;
  }

  public getActionRoles(): Observable<ActionRoles[]> {
    if (!this.hasActionRoles) {
      this.fetchUser();
    }
    return this.actionRolesSubject.asObservable();
  }

  public fetchUser() {
    console.log('fetch user');
    this.httpService.getData<ActionRoles[]>(this.api.getActionRoles()).subscribe(actionRoles => {

      this.hasActionRoles = true;
      this.actionRolesSubject.next(actionRoles);
    },
      (error) => {
        this.hasActionRoles = false;
        this.actionRolesSubject.error(error);
      });
  }

  public async getactionroles() {
    console.log('get action roles');
    this.actionRoles = await this.httpService.getData<ActionRoles[]>(this.api.getActionRoles()).toPromise();    
  }

  public async  sleep(msec) {
    return new Promise(resolve => setTimeout(resolve, msec));
  }

}
