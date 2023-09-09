import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(), //ANGULAR DROPDOWN
    TabsModule.forRoot(), //ANGULAR MODULE
    ToastrModule.forRoot({ // ANGULAR TOASTR
      positionClass: 'toast-bottom-right',
    })
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    TabsModule
  ]
})
export class SharedModule { }
