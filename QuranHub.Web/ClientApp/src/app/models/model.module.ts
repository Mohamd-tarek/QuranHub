import { NgModule } from "@angular/core";
import { PostRepository } from "../abstractions/repositories/postRepository";
import { QuranRepository } from "../abstractions/repositories/quranRepository";
import { QuranDataRepository } from "./repositories/quranDataRepository.model";
import { PostDataRepository } from "./repositories/postDataRepository.model";
import { CommonFuntionality } from "./commonFunctionality.service";
import { NotificationRepository } from '../abstractions/repositories/notificationRepository';
import { NotifcationDataRepository } from './repositories/NotificationDataRepository.model';


@NgModule({
  imports: [],
  providers: [
     CommonFuntionality,
    { provide: QuranRepository, useClass: QuranDataRepository },
    { provide: PostRepository, useClass: PostDataRepository },
    { provide: NotificationRepository, useClass: NotifcationDataRepository }
          ]
})

export class ModelModule {}
