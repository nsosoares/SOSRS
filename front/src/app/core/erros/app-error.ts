export class AppError extends Error {

  public constructor(message?: string, public innerError?: unknown, public innerMessage: object | undefined = undefined) {
      const errorColor = 'color: red';
      console.log('%cErro inesperado', errorColor);
      console.log('%c' + message, errorColor);

      if (innerError){
        console.log('%cInnerError', errorColor);
        console.log(innerError);
      }
      if (innerMessage) {
        console.log('%cmessageComplementar', errorColor);
        console.log(innerMessage);
      }
      super(message, {cause: innerError});
      this.innerError = innerError;
  }

  public static fromBussiness(message: string, complement: any): AppError {
      const errorColor = 'color: red';
      console.log('%cParametro de referencia:', errorColor);
      console.log(complement);
      return new AppError(message);
  }
}
