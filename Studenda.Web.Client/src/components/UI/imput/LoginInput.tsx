import React, { FC } from 'react'
import classes from './LoginInput.module.css'

interface inputProps {
        text?: string;
}
const LoginInput: FC<inputProps> =
  ({
    text, 
  }) => {

  return (
       <input placeholder={text} className={classes.LoginInput}></input>
  )
}

export default LoginInput;
