import { Component, OnInit } from '@angular/core';
import { CreateOperation } from '../../../models/create-operation.model';
import { FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { OperationsService } from '../../../services/operations.service';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { CommonModule } from '@angular/common';
import { OperationCategoriesService } from '../../../services/operation-categories.service';
import { OperationCategory } from '../../../models/operation-category.model';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router } from '@angular/router';
import { Account } from '../../../models/account.model';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';

@Component({
  selector: 'app-add-operation',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatChipsModule,
    MatSelectModule,
    MatIconModule,
    MatInputModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './add-operation.component.html',
  styleUrl: './add-operation.component.css'
})
export class AddOperationComponent implements OnInit {
  isLoadingForm: boolean = false;

  paginationSettings: PaginationSettings = new PaginationSettings();

  operationForm: FormGroup;
  descriptionArray: FormGroup[] = [];
  account!: Account;

  addedFields: { key: string, value: any }[] = [];

  constructor(
    private operationsService: OperationsService, 
    private categoryService: OperationCategoriesService,
    private accountService: FinancialAccountsService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = this.route.parent?.snapshot.paramMap.get('id');
    console.log("id " + id);

    if (id) {
      this.loadAccount(id);
    }

    this.operationForm = this.formBuilder.group({
      category: ['', Validators.required],
      amount: ['', Validators.required],
      description: this.formBuilder.array([])
    });
  }

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

  loadAccount(id: string) {
    this.accountService.getAccountById(id).subscribe(
      account => {
        this.account = account;
      }
    )
  }

  addField() {
    const descriptionArray = this.operationForm.get('description') as FormArray;
    const newFieldGroup = this.formBuilder.group({
      key: [''],
      value: ['']
    });
    descriptionArray.push(newFieldGroup);
    this.addedFields.push({ key: '', value: '' });
  }
  
  removeField(index: number) {
    const descriptionArray = this.operationForm.get('description') as FormArray;
    descriptionArray.removeAt(index);
    this.addedFields.splice(index, 1);
  }
  
  createForm(): FormGroup {
    return this.formBuilder.group({
      key: ['', Validators.required],
      value: ['', Validators.required]
    });
  }

  createDescriptionObject(): { [key: string]: any } {
    const descriptionArray = this.operationForm.get('description') as FormArray;
    const description: { [key: string]: any } = {};

    this.addCustomFieldsToDesc(descriptionArray);
  
    for (let i = 0; i < descriptionArray.length; i++) {
      const fieldGroup = descriptionArray.at(i) as FormGroup;
      const key = fieldGroup.get('key')?.value;
      const value = fieldGroup.get('value')?.value;
  
      if (key && value) {
        description[key] = value;
      }
    }
  
    return description;
  }

  addCustomFieldsToDesc(descriptionArray: FormArray) {
    const formGroup1 = this.createForm();
    formGroup1.patchValue({
      key: 'CategoryId',
      value: this.operationForm.get('category')!.value.id
    });
    descriptionArray.push(formGroup1);
  
    const formGroup2 = this.createForm();
    formGroup2.patchValue({
      key: 'Amount',
      value: this.operationForm.get('amount')!.value
    });
    descriptionArray.push(formGroup2);
  }

  addOperation() {
    if (this.operationForm.valid) {
      const description = this.createDescriptionObject();

      const newOperation: CreateOperation = {
        accountId: this.account.id,
        account: this.account,
        description: description
      };

      this.operationsService.addOperation(newOperation).subscribe(
        response => {
          this.router.navigate(['./profile/{{userId}}']);
          console.log("new o " + newOperation);
        }
      );
    }
  }
}
