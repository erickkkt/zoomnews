import { ValidatorFn, AbstractControl } from '@angular/forms';

export function postcodenotfound(address1Control: any, address3Control: any, isAddressFound: any): ValidatorFn {
    return (c: AbstractControl): { [key: string]: boolean } | null => {
        if (isAddressFound && isAddressFound.value) {
            return null;
        }
        if (c.value && address1Control.value && address3Control.value) {
            return null;
        }
        return { 'postcodenotfound': true };
    };
}