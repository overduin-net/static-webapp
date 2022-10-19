export class BaseService {
  constructor() {
  }

  protected transformOptions(options: any) {
    return Promise.resolve(options);
  }

  // Not implemented yet
  protected transformResult(url: string, response: Response, processor: (response: Response) => any) {
    return processor(response);
  }
}