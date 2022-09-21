import java.io.File;
import java.io.IOException;
import java.nio.charset.Charset;
import java.util.List;

import org.apache.commons.io.FileUtils;
import org.json.JSONArray;

class Program {
    public static void main(String[] args)
    {
        try 
        {
        }catch(Exception ex){

        }
    }

    public static void printList(List<String> enumList){
        File EnumFile = new File("D:\\Work\\FeatureProgram\\Templates\\java\\Enum.txt");
		for(int iter=0;iter<enumList.size();iter++){
			// System.out.println(enumList.get(iter)+" "+iter);
			try {
				FileUtils.writeStringToFile(EnumFile,enumList.get(iter)+":"+iter+";",(Charset)null,true);
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}

	public static void ReadFile(){
		File enumFile = new File("D:\\Work\\FeatureProgram\\Templates\\java\\Enum.txt");
		try {
			String fileStr = FileUtils.readFileToString(enumFile, (Charset)null);
			String[] arr = fileStr.split(";");
			for(int iter=0;iter<arr.length;iter++){
				System.out.println(arr[iter]);
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

    public static JSONArray convertInvalidJSONArrayToValidArray(String jsonString){
		JSONArray eventArray = null;
		try{
			int index = jsonString.lastIndexOf("},");
			if(index != -1) {
				StringBuilder strBuilder = new StringBuilder();
				strBuilder.append(jsonString.substring(0, index));
				strBuilder.append("}]");
				try {
					eventArray = new JSONArray(strBuilder.toString());
					jsonString = strBuilder.toString();
				} catch (Exception ex) {
					ex.printStackTrace();
				}
			}
		}catch (Exception ex){
			ex.printStackTrace();
		}
		return eventArray;
	}
}