import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';

@Component({
  selector: 'app-article-card',
  standalone: true,
  imports: [
    MatTabsModule, 
    CommonModule
  ],
  templateUrl: './article-card.component.html',
  styleUrl: './article-card.component.css'
})
export class ArticleCardComponent implements OnInit {
  articles: any;

  ngOnInit(): void {
    this.articles = [
      { 
        title: '10 Tips for Personal Finance Management', 
        publicationDate: 'Published on May 15, 2024',
        description: 'In this article, we will share with you 10 tips for managing personal finances:', 
        content: [ "Make a budget and stick to it.", 
                   "Set aside a small amount of money each month.",  
                   "Pay off your debts gradually.",
                   "Invest in your future.",
                   "Insure yourself and your family.", 
                   "Study and improve your financial literacy.", 
                   "Plan large purchases and avoid impulsive spending.",
                   "Evaluate and optimize your expenses.",
                   "Create an emergency fund in case of unexpected expenses.",
                   "Seek financial advice from professionals."
                  ],
        link: 'https://medium.com/@d.w.norrisjr/10-tips-for-managing-your-personal-finances-bf8e9bd9fa84' 
      },
      { 
        title: 'How to create a debt repayment plan', 
        publicationDate: 'Published on May 25, 2024',
        description: 'In this article, we will tell you how to create an effective debt repayment plan:', 
        content: [ "Make a list of all your debts.", 
                   "Determine the repayment amounts for each debt.",  
                   "Set priorities and determine which debt to repay first.",
                   "Consider refinancing debt with lower interest rates.",
                   "Reduce unnecessary expenses and allocate more money to pay off debts.", 
                   "Use a 'snowball' strategy to speed up debt repayment.", 
                   "Constantly monitor your progress and make adjustments to the plan if necessary."
                  ],
        link: 'https://medium.com/@kimhengsok.info/how-to-create-a-debt-repayment-plan-b8add18caeff' 
      }
    ];
  }
}
