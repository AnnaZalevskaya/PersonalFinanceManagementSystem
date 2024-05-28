import { Component, OnInit } from '@angular/core';
import { CreateOperation } from '../../models/create-operation.model';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Account } from '../../models/account.model';
import { OperationsService } from '../../services/operations.service';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';
import { CommonModule } from '@angular/common';
import { OperationCategoriesService } from '../../services/operation-categories.service';
import { OperationCategory } from '../../models/operation-category.model';
import { PaginationSettings } from '../../settings/pagination-settings';
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-add-operation',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatChipsModule,
    MatIconModule,
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

  paginationSettings: PaginationSettings = new PaginationSettings();

  constructor(
    private operationsService: OperationsService, 
    private categoryService: OperationCategoriesService
  ) {}

  fields: { key: string, value: string }[] = [];
  categories: OperationCategory[] = [];
  category!: OperationCategory;
  amount: number = 0;

  ngOnInit(): void {
    this.loadCategories();
    this.isLoadingForm = true;
  }

  loadCategories() {
    this.categoryService.getCategories(this.paginationSettings).subscribe(
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

  keywords: { value: string, status: string }[] = [];
  formControl = new FormControl(['angular']);

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value.trim();

    if (value) {
      this.keywords.push({ value: value, status: 'active' });
    }

    if (input) {
      input.value = '';
    }
  }

  removeKeyword(keyword: { value: string, status: string }): void {
    const index = this.keywords.indexOf(keyword);
    if (index >= 0) {
      this.keywords.splice(index, 1);
    }
  }

  trackByKeyword(index: number, keyword: { value: string, status: string }): string {
    return keyword.value;
  }
}
