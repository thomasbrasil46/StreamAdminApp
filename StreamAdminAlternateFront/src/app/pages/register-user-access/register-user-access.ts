import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MasterService } from '../../services/master-service';
import { Router } from '@angular/router';

@Component({
  imports: [FormsModule],
  selector: 'app-register-user-access',
  styleUrl: './register-user-access.css',
  templateUrl: './register-user-access.html',
})
export class RegisterUserAccess {

  newUserAccessObj: any = {    
  "id": 0,
  "userFullName": "",
  "userEmail": "",
  "userPassword": ""
  };

  masterService = inject(MasterService);
  router = inject(Router);

  onSaveUser(){
    debugger;
    this.masterService.onRegisterUser(this.newUserAccessObj).subscribe({
      next:(res:any)=>{
        debugger;
        if(res.result){
          alert("Usuário cadastrado com sucesso!");
          this.router.navigateByUrl('/login');
        } else{
          alert("Erro ao cadastrar usuário: " + res.message);
        }
      },
      error:(err:any)=>{
        alert("Erro ao cadastrar usuário: " + err.message);
      }
    });
  }
}
