import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0; //REQUEST TAKING PLACE DISPLAY SPINNER >0

  constructor(private spinnerServer: NgxSpinnerService) { }

  busy() {
    this.busyRequestCount++;
    this.spinnerServer.show(undefined, {
      type: 'line-scale-party',
      bdColor: 'rgba(255,255,255,0)',
      color: '#333333'
    })
  }

  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinnerServer.hide();
    } // MUST NEED INTERCEPTOR TO ACTIVATE LOADING HTTP REQUEST
  }
}
