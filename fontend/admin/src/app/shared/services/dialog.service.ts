import { Component, Injectable, ViewContainerRef, TemplateRef } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material';
import { Observable } from 'rxjs';
import { DialogComponent } from '../components/dialog/dialog.component';


@Injectable({
    providedIn: 'root'
})
export class DialogService {

    constructor(private dialog: MatDialog) {
    }

    public openSuccessDialog(data) {
        data['type'] = 1; // success
        this.openDialog(data);
    }

    public openSuccessDialogConfirm(data) : Observable<any> {
        data['type'] = 1; // success
        return this.openDialog(data).afterClosed();
    }

    public openErrorDialog(data) {
        data['type'] = 2; // 2:error
        const dialogRef = this.openDialog(data);
        dialogRef.keydownEvents().subscribe(e => {
            if (e.keyCode === 13) {
                e.preventDefault();
                e.stopPropagation();
                setTimeout(() => {
                    dialogRef.close();
                }, 1000);
            }
        });
    }

    public openErrorDialogConfirm(data) : Observable<any> {
        data['type'] = 2; // 2:error
        const dialogRef = this.openDialog(data);
        dialogRef.keydownEvents().subscribe(e => {
            if (e.keyCode === 13) {
                e.preventDefault();
                e.stopPropagation();
                setTimeout(() => {
                    dialogRef.close();
                }, 1000);
            }
        });
        return dialogRef.afterClosed();
    }

    public openConfirmationDialog(data): Observable<any> {
        data['type'] = 3; // confirm
        data['yesOption'] = false;
        let dialogConfig = this.getDialogConfig();
        dialogConfig.data = data;
        let dialogRef = this.dialog.open(DialogComponent, dialogConfig);
        return dialogRef.afterClosed();
    }

    public getDialogConfig() {
        let config = new MatDialogConfig();
        config.disableClose = true;
        config.width = '600px';
        return config;
    }

    public openComponentDialog(componentOrTemplateRef, data?: Object, config?: MatDialogConfig): Observable<any> {
        let dialogConfig = Object.assign({}, this.getDialogConfig(), config);
        if (data) {
            dialogConfig.data = data;
        }
        let dialogRef = this.dialog.open(componentOrTemplateRef, dialogConfig);
        return dialogRef.afterClosed();
    }

    openDialog(data) {
        let dialogConfig = this.getDialogConfig();
        dialogConfig.disableClose = false;
        dialogConfig.autoFocus = true;
        dialogConfig.data = data;
        let dialogRef = this.dialog.open(DialogComponent, dialogConfig);
        return dialogRef;
    }
}

