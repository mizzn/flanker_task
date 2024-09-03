chose_by_incongruency = function(data, incongruency) {
  if(incongruency == "congruent"){
    chosen_data = data[data$stimulus == "leftCongruent" | data$stimulus == "rightCongruent", ]
  }else if(incongruency == "incongruent"){
    chosen_data = data[data$stimulus == "leftIncongruent" | data$stimulus == "rightIncongruent", ]
  }else{
    return("ERROR")
  }
  
  return(chosen_data)
}

remove_unnecessary_data = function(data){
  # RTの平均と標準偏差を計算
  ave = mean(data$RT, na.rm = TRUE)
  sd = sd(data$RT, na.rm = TRUE)
  # print(ave)
  # print(sd)
  
  # print((ave - 2 * sd))
  # print((ave + 2 * sd))
  
  # 離れたやつを除く
  filtered_data = data[(ave - 2 * sd) <= data$RT & data$RT <= (ave + 2 * sd),]
  
  # 正解データのみ
  filtered_data = filtered_data[filtered_data$TF == 1,]
  
  return(filtered_data)
}

add_row = function(df, displayformat, incongruency, RT_ave){
  new_row <- data.frame(
    displayformat = displayformat,
    incongruency = incongruency,
    RT_ave = RT_ave
  )
  df <- rbind(df, new_row)
  return(df)
}
add_row_test = function(df, displayformat, incongruency, RT_ave, condition){
  new_row <- data.frame(
    displayformat = displayformat,
    incongruency = incongruency,
    RT_ave = RT_ave,
    condition = condition
  )
  df <- rbind(df, new_row)
  return(df)
}

get_display_format = function(file_name){
  if(file_name == "controll"){
    display_format = "VR Object"
  }
  else if(file_name == "2D"){
    display_format = "2D Object"
  }else if(file_name == "barrier"){
    display_format = "VR Object with barrier"
  }else if(file_name == "Far"){
    display_format = "VR Object with distance"
  }else{
    display_format = "ERROR"
  }
  return(display_format)
}

