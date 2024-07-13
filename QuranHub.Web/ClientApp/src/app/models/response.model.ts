import { ResponseCodeEnum } from "./enums";

export class Response<T> {
  code!: ResponseCodeEnum;
  message!: string;
  data!: T;
  items: any;
}
