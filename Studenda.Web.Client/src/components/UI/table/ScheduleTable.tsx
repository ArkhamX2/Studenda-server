import React, { FC, useEffect, useState } from 'react'
import  './ScheduleTable.module.css'
import classes from './ScheduleTable.module.css'

export enum row {
  half='half',
  split='split'
}

const ScheduleTable: FC =
  () => {

  return (
        <table>
                <tr>
                        <td className={classes.Row1} style={{width:'75px'}}>

                        </td>

                        <td className={classes.Row1}>
                                Понедельник
                        </td>

                        <td className={classes.Row1}>
                                Вторник
                        </td>

                        <td className={classes.Row1}>
                                Среда
                        </td >

                        <td className={classes.Row1}>
                                Четверг
                        </td>

                        <td className={classes.Row1}>
                                Пятница
                        </td>

                        <td className={classes.Row1}>
                                Суббота
                        </td>

                </tr>

                <tr>
                        <td className={classes.Column1}>
                                1
                        </td>
                        <td>
                                4
                        </td>

                        <td>
                                5
                        </td>

                        <td>
                                6
                        </td>

                        <td>
                                4
                        </td>

                        <td>
                                5
                        </td>

                        <td>
                                6
                        </td>

                </tr>

               

        </table>
       //<input placeholder={text} className={classes.LoginInput}  style={{display: 'flex', textAlign: align===TextAlign.right  ? 'right' : 'center'}}></input>
  )
}

export default ScheduleTable;
