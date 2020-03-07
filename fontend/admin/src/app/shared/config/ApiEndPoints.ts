import { Injectable } from '@angular/core';
import { configuration } from 'src/environments/environment';
import { ConfigurationService } from 'src/app/services/configuration.service';
import { Utility } from 'src/app/utils/utility';

@Injectable({
    providedIn: 'root'
})
export class ApiEndPoints {

    constructor(private readonly configurationService: ConfigurationService) { }


    // getPricingsPaging(searchString: string, isIncludedInactive: boolean, sortField: string, sortDirection: string, pageNumber: number, pageSize: number) {
    //     return `${this.configurationService.apiBaseUrl}/api/${configuration.version}/pricing/${Utility.encodeSearchString(searchString)}/${isIncludedInactive}/${sortField}/${sortDirection}/${pageNumber}/${pageSize}`;
    // }

    getActionRoles() {
        return `${this.configurationService.apiBaseUrl}/api/${configuration.version}/action-roles`;
    }

    getAuthorizedNavItems() {
        return `${this.configurationService.apiBaseUrl}/api/${configuration.version}/action-roles/authorized-nav-items`;
    }

}
