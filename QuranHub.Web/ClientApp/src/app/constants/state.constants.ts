
import { RestApiUrl } from "./application.constants";

interface RestApiPathsType  {
   readonly SessionURL: string,
}

export const restApiPaths: RestApiPathsType = {
  SessionURL: `${RestApiUrl }/Session/`,
}







