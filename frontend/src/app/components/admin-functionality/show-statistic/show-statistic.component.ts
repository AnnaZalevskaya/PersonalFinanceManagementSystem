import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { AccountType } from '../../../models/account-type.model';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { AccountTypesService } from '../../../services/account-types.service';
import { AccountStatistic } from '../../../models/account-statistic.model';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { NgxChartsModule } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-show-statistic',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule, 
    MatSelectModule,
    MatCardModule,
    NgxChartsModule
  ],
  templateUrl: './show-statistic.component.html',
  styleUrl: './show-statistic.component.css'
})
export class ShowStatisticComponent implements OnInit {
  selectedType!: AccountType;
  statisticInfo: AccountStatistic[] = [];
  types: AccountType[] = [];
  chartData: any[] = [];

  typePaginationSettings: PaginationSettings = new PaginationSettings();

  isResult: boolean = false;

  constructor(private accountService: FinancialAccountsService, private typeService: AccountTypesService) 
  {
    
  }

  ngOnInit(): void {
    this.typePaginationSettings.pageNumber = 1;
    this.typePaginationSettings.pageSize = 2;
     
    this.loadAccountTypes();
  }

  loadAccountTypes() {
    this.typeService.getTypes(this.typePaginationSettings).subscribe((types: AccountType[]) => {
      this.types = types;
    });
  }

  getStatistics() {
    this.accountService.getAccountStatistics(this.selectedType.id.toString()).subscribe(
      response => {
        this.statisticInfo = response;
        this.chartData = [];
        this.statisticInfo.forEach(element => {
          this.chartData.push(
            { name: element.currency, value: element.amount }
          );
        });

        this.isResult = true;
      }
    )
  } 
}
