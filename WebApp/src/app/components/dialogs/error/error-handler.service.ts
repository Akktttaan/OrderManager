import { Injectable } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {Subject} from "rxjs";
import {ErrorComponent} from "./error.component";

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor(private dialog: MatDialog) { }

  public showError = (errorStream: Subject<string>) => {
    errorStream.subscribe(errorMessage => {
      // Отображаем сообщение об ошибке в диалоговом окне
      this.dialog.open(ErrorComponent, {
        width: '400px',
        data: {
          message: errorMessage
        }
      })
    });
  }
}
