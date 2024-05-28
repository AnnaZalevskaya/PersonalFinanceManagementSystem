import { Component } from '@angular/core';
import { ArticleCardComponent } from '../article-card/article-card.component';
import { FooterComponent } from "../footer/footer.component";
import { TemplatesCarouselComponent } from "../templates-carousel/templates-carousel.component";

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './main.component.html',
    styleUrl: './main.component.css',
    imports: [
      ArticleCardComponent,
      FooterComponent,
      TemplatesCarouselComponent
    ]
})
export class MainComponent {

}