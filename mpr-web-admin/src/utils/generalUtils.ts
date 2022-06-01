export const getFontBaseValue = (defaultFontSize = 16): number => {
  return defaultFontSize;
};
  
export const toREM = (value: number): string => {
  const baseValue = getFontBaseValue();
  return `${value * (1 / baseValue)}rem`;
};

export const getDefaultValue = (value: any, defaultValue: any = '-') => (isEmpty(value) ? defaultValue : value);

export const getConditionalDefaultValue = (condition: any, value: any, defaultValue: any = '-') =>
  !!condition ? getDefaultValue(value, defaultValue) : defaultValue;

export const isObject = (entity : any) => entity && typeof entity === 'object' && entity.constructor === Object;

export const isArray = (value : any) => Array.isArray(value);

export const isUndefined = (value : any) => value === undefined;

export const isNull = (value : any) => value === null;

export const isNumber = (value : any) => typeof value === 'number';
  
export const isEmpty = (value : any) => {
  if (isObject(value)) {
    return Object.keys(value).length === 0;
  }
  if (isUndefined(value) || isNull(value)) {
    return true;
  }
  if (isArray(value)) {
    return value.length === 0;
  }
  if (isNumber(value)) {
    return Number.isNaN(value);
  }

  return value === '';
};

export const getLanguage = (laguangeCode : string) : string => {
  switch(laguangeCode){
    case 'en':
      return 'English';
    default:
      return laguangeCode;
  }    
}