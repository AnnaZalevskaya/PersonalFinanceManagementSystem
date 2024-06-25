import { Component } from '@angular/core';
import { ArticleCardComponent } from '../../main-pages/article-card/article-card.component';
import { FooterComponent } from "../../main-pages/footer/footer.component";
import { TemplatesCarouselComponent } from "../../main-pages/templates-carousel/templates-carousel.component";

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