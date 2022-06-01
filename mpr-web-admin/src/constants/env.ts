const config = process.env;

export const ENV = {
  ENVIRONMENT: config.REACT_APP_ENV,
  IS_DEV: config.REACT_APP_ENV === 'development',
  API: {
    URL: config.REACT_APP_API_URL as string,
    MAX_RETRIES: 3,
    RETRY_TIMEOUT: 1000,
  }
};