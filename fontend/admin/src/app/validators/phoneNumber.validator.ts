import { AbstractControl } from '@angular/forms';

export function isValidPhoneNumber(control: AbstractControl) {
    let pattern = /^[0][1-9]\d{9}$|^[1-9]\d{9}$/;

    if (control.value && !pattern.test(control.value)) {
        return { 'invalidPhoneNumber': true };
    }
    return null
};
