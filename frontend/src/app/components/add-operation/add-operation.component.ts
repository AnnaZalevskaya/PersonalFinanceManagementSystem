import { Component, OnInit } from '@angular/core';
import { CreateOperation } from '../../models/create-operation.model';
import { FormsModule } from '@angular/forms';
import { Account } from '../../models/account.model';
import { OperationsService } from '../../services/operations.service';

@Component({
  selector: 'app-add-operation',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-operation.component.html',
  styleUrl: './add-operation.component.css'
})
export class AddOperationComponent {
  accountId: number = 0;
  account!:  Account;
  description!: {};

  constructor(private operationsService: OperationsService) {}

  addOperation() {
    const newOperation: CreateOperation = {
      accountId: this.accountId,
      account: this.account,
      description: this.description
    };

    this.operationsService.addOperation(newOperation);
  }
}
