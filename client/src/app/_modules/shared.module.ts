import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(), //ANGULAR DROPDOWN
    TabsModule.forRoot(), //ANGULAR MODULE
    ToastrModule.forRoot({ 
      positionClass: 'toast-bottom-right',
    }), // ANGULAR TOASTR
    NgxSpinnerModule.forRoot({
      type: 'line-scale-party'
    }) // ANGULAR SPINNER
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    NgxSpinnerModule //ADD SERVICE INTERCEPTOR IN BETWEEN HTTP REQUEST
  ]
})
export class SharedModule { }
