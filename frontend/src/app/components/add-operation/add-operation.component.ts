import { Component, OnInit } from '@angular/core';
import { CreateOperation } from '../../models/create-operation.model';
import { FormsModule } from '@angular/forms';
import { Account } from '../../models/account.model';
import { OperationsService } from '../../services/operations.service';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';
import { CommonModule } from '@angular/common';
import { OperationCategoriesService } from '../../services/operation-categories.service';
import { OperationCategory } from '../../models/operation-category.model';

@Component({
  selector: 'app-add-operation',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './add-operation.component.html',
  styleUrl: './add-operation.component.css'
})
export class AddOperationComponent implements OnInit {
  isLoadingForm: boolean = false;
  accountId: number = 0;
  account!:  Account;
  description!: {};

  constructor(private operationsService: OperationsService, private categoryService: OperationCategoriesService) {}

  fields: { key: string, value: string }[] = [];
  categories: OperationCategory[] = [];
  category!: OperationCategory;
  amount: number = 0;
  operationDictionary: { [key: string]: string | number } = {};

  ngOnInit(): void {
    this.loadCategories();
    this.isLoadingForm = true;
  }

  loadCategories() {
    this.categoryService.getTypes().subscribe(
      categories => {
        this.categories = categories;
      }
    )
  }

  addField() {
    this.fields.push({ key: '', value: '' });
  }

  removeField(index: number) {
    this.fields.splice(index, 1);
  }

  addOperation() {
    const newOperation: CreateOperation = {
      accountId: this.accountId,
      account: this.account,
      description: this.description
    };

    this.operationsService.addOperation(newOperation);
  }
}
