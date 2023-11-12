import React, { FC } from 'react'
import classes from './LoginInput.module.css'
export enum TextAlign {
  right='right',
  center='center'
}

interface inputProps {
        text?: string;
        align?: TextAlign;
}
const LoginInput: FC<inputProps> =
  ({
    text, 
    align
  }) => {

  return (
       <input placeholder={text} className={classes.LoginInput}  style={{display: 'flex', textAlign: align===TextAlign.right  ? 'right' : 'center'}}></input>
  )
}

export default LoginInput;
