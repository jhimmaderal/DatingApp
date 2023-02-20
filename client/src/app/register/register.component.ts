import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() userFromHomeComponent: any; //input decorator for parent to child 
  @Output() cancelRegister = new EventEmitter(); // input decorator for child to parent
  model: any = {}

  constructor(private accountService: AccountService) { //inject Account Service 
  }

  ngOnInit(): void {
  }

  register() { // add account from database
    this.accountService.register(this.model).subscribe({
      next: () => {
        // console.log(response); 
        this.cancel();
      },
      error: error => console.log(error)
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
