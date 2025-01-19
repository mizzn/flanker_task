library(effectsize)
library(emmeans)

source("functions.R")

# formからのデータをよみだす
form_data = read.csv("csv/JNNS2024_flanker_answer_test.csv", header = TRUE, sep = ",")
# View(form_data)

# ID(ファイル名)を取得
ids = form_data[, 7]
print(ids)
# print(id[2])

# 分析用dfを作っておく
df = data.frame(
  displayFormat = character(), # 表示形式
  incongruency = character(), # 一致不一致
  RT_ave = numeric(), # RT
  condirion = character() # 速さか正確性か
)
View(df)

file_names = c("controll", "2D", "barrier", "Far")
# display_formats = c("VR Object", "2D Object", "VR Object with barrier", "VR Object with distance)
# id = "ZAQJ"

for (id in ids) {
  for (file_name in file_names){
    display_format = get_display_format(file_name)
    if(id == "286" | id == "351"){
      condition = "speed"
    }else if(id == "205" | id == "885"){
      condition = "quality"
    }else{
      condition = "ERROR"
    }
    # パスつくる
    path = paste0("csv/", id, "/")
    # print(path)
    
    # csv読み出して確認
    response_data = read.csv(paste0(path, id, file_name,".csv"), header = TRUE, sep = ",")
    paste0(path, id, file_name,".csv")
    # View(response_data)
    
    # 要因ごとに処理
    congruent_data = chose_by_incongruency(response_data, "congruent")
    # View(congruent_data)
    filtered_congruent = remove_unnecessary_data(congruent_data)
    # View(filtered_congruent)
    congruent_ave = mean(filtered_congruent$RT, na.rm = TRUE)
    df = add_row_test(df, display_format, "congruent", congruent_ave, condition)
    # View(df)
    
    incongruent_data = chose_by_incongruency(response_data, "incongruent")
    filtered_incongruent = remove_unnecessary_data(incongruent_data)
    # View(filtered_data)
    incongruent_ave = mean(filtered_incongruent$RT, na.rm = TRUE)
    df = add_row_test(df, display_format, "incongruent", incongruent_ave, condition)
    # View(df)
    
  }
}

View(df)

# AOV <- aov(RT_ave ~ incongruency + displayformat + incongruency * displayformat, data = df)
AOV <- aov(RT_ave ~ incongruency * displayformat * condition, data = df)
summary(AOV)

eta_squared(AOV)

emmip(AOV, incongruency ~ displayformat, CIs = TRUE)
